using Microsoft.Data.SqlClient;
using ST_BE.Dtos;
using ST_BE.Models;
using System.Data;

namespace ST_BE.Data
{
    // Lỗi nghiệp vụ (hết hàng, không đủ điểm...) -> controller trả về 400
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }

    // Truy cập CSDL bằng ADO.NET thuần (SqlConnection / SqlCommand / SqlDataReader / SqlTransaction).
    // Dùng cho các chỗ cần tốc độ hoặc cần kiểm soát giao dịch chặt chẽ; phần CRUD thông thường vẫn dùng EF Core.
    // Mọi giá trị đầu vào đều truyền qua tham số (@...) — không bao giờ cộng chuỗi SQL -> chống SQL Injection.
    public class AdoNetRepository
    {
        public const decimal MoneyPerPoint = 10_000;   // Mua 10.000đ được 1 điểm
        public const decimal PointValue = 1_000;       // 1 điểm đổi được 1.000đ

        private readonly string _connectionString;

        public AdoNetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection trong appsettings.json");
        }

        // ===== Tra cứu mã vạch: SqlConnection + SqlCommand + SqlDataReader =====
        // Máy quét đọc mã vạch EAN in trên bao bì (cột Barcode); gõ tay thì thường là mã hàng nội bộ (ProductCode)
        public async Task<ProductDto?> GetProductByCodeAsync(string code)
        {
            const string sql = @"
                SELECT p.ProductId, p.ProductCode, p.Barcode, p.ProductName, p.CategoryId, c.CategoryName,
                       p.Unit, p.CostPrice, p.SalePrice, p.StockQuantity, p.Description, p.IsActive
                FROM Products p
                JOIN Categories c ON c.CategoryId = p.CategoryId
                WHERE (p.Barcode = @Code OR p.ProductCode = @Code) AND p.IsActive = 1";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Code", code.Trim().ToUpper());
            await conn.OpenAsync();

            // SqlDataReader đọc một chiều (forward-only), nhanh và tốn ít bộ nhớ
            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            return new ProductDto
            {
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                Barcode = reader.IsDBNull(reader.GetOrdinal("Barcode")) ? null : reader.GetString(reader.GetOrdinal("Barcode")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit")),
                CostPrice = reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                SalePrice = reader.GetDecimal(reader.GetOrdinal("SalePrice")),
                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        // ===== Thanh toán: lưu hóa đơn + trừ tồn kho + trừ/cộng điểm trong CÙNG 1 SqlTransaction =====
        // Bất kỳ bước nào lỗi (hết hàng, không đủ điểm...) -> transaction.Rollback(), CSDL không có hóa đơn rác.
        public async Task<int> CreateOrderAsync(int userId, CreateOrderRequest request)
        {
            var items = request.Items.GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) }).ToList();

            if (request.RedeemPoints > 0 && request.CustomerId == null)
                throw new BusinessException("Chỉ khách hàng thân thiết mới được dùng điểm");

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 1. Khóa dòng khách hàng (UPDLOCK) để 2 quầy không cùng tiêu một số điểm
                int customerPoints = 0;
                if (request.CustomerId.HasValue)
                {
                    using var cmd = new SqlCommand("SELECT Points FROM Customers WITH (UPDLOCK, ROWLOCK) WHERE CustomerId = @CustomerId", conn, transaction);
                    cmd.Parameters.AddWithValue("@CustomerId", request.CustomerId.Value);
                    var result = await cmd.ExecuteScalarAsync();
                    if (result == null) throw new BusinessException("Khách hàng không tồn tại");
                    customerPoints = (int)result;
                    if (request.RedeemPoints > customerPoints)
                        throw new BusinessException($"Khách chỉ có {customerPoints:N0} điểm, không đủ để dùng {request.RedeemPoints:N0} điểm");
                }

                // 2. Đọc giá bán / giá vốn hiện tại (khóa dòng sản phẩm tới khi kết thúc giao dịch)
                var lines = new List<(int ProductId, string Name, decimal Price, decimal Cost, int Quantity)>();
                foreach (var item in items)
                {
                    using var cmd = new SqlCommand(@"
                        SELECT ProductName, SalePrice, CostPrice, StockQuantity, IsActive, Unit
                        FROM Products WITH (UPDLOCK, ROWLOCK) WHERE ProductId = @ProductId", conn, transaction);
                    cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                    using var reader = await cmd.ExecuteReaderAsync();
                    if (!await reader.ReadAsync() || !reader.GetBoolean(4))
                        throw new BusinessException($"Mặt hàng (ID {item.ProductId}) không tồn tại hoặc đã ngừng kinh doanh");

                    var name = reader.GetString(0);
                    var stock = reader.GetInt32(3);
                    if (stock < item.Quantity)
                        throw new BusinessException($"'{name}' chỉ còn {stock} {reader.GetString(5)} trong kho");

                    lines.Add((item.ProductId, name, reader.GetDecimal(1), reader.GetDecimal(2), item.Quantity));
                }

                // 3. Tính tiền
                var subTotal = lines.Sum(l => l.Price * l.Quantity);
                var pointsDiscount = request.RedeemPoints * PointValue;
                if (request.Discount + pointsDiscount > subTotal)
                    throw new BusinessException("Tổng giảm giá và tiền đổi điểm không được lớn hơn tổng tiền hàng");
                var total = subTotal - request.Discount - pointsDiscount;
                if (request.CustomerPaid < total)
                    throw new BusinessException($"Khách đưa chưa đủ tiền (cần {total:N0}đ)");

                // 4. Lưu hóa đơn, lấy lại khóa chính vừa sinh bằng OUTPUT INSERTED
                int orderId;
                using (var cmd = new SqlCommand(@"
                    INSERT INTO Orders (OrderCode, CreatedAt, UserId, CustomerId, SubTotal, Discount, PointsUsed, PointsDiscount, TotalAmount, CustomerPaid, Note, Status)
                    OUTPUT INSERTED.OrderId
                    VALUES (@OrderCode, @CreatedAt, @UserId, @CustomerId, @SubTotal, @Discount, @PointsUsed, @PointsDiscount, @TotalAmount, @CustomerPaid, @Note, @Status)",
                    conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@OrderCode", $"HD{DateTime.Now:yyMMddHHmmssfff}");
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CustomerId", (object?)request.CustomerId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SubTotal", subTotal);
                    cmd.Parameters.AddWithValue("@Discount", request.Discount);
                    cmd.Parameters.AddWithValue("@PointsUsed", request.RedeemPoints);
                    cmd.Parameters.AddWithValue("@PointsDiscount", pointsDiscount);
                    cmd.Parameters.AddWithValue("@TotalAmount", total);
                    cmd.Parameters.AddWithValue("@CustomerPaid", request.CustomerPaid);
                    cmd.Parameters.AddWithValue("@Note", (object?)request.Note?.Trim() ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", OrderStatus.Completed);
                    orderId = (int)(await cmd.ExecuteScalarAsync())!;
                }

                // 5. Lưu chi tiết + trừ tồn kho. Điều kiện "StockQuantity >= @Quantity" chặn bán âm kho
                foreach (var l in lines)
                {
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO OrderDetails (OrderId, ProductId, ProductName, UnitPrice, UnitCost, Quantity, LineTotal)
                        VALUES (@OrderId, @ProductId, @ProductName, @UnitPrice, @UnitCost, @Quantity, @LineTotal)", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", l.ProductId);
                        cmd.Parameters.AddWithValue("@ProductName", l.Name);
                        cmd.Parameters.AddWithValue("@UnitPrice", l.Price);
                        cmd.Parameters.AddWithValue("@UnitCost", l.Cost);
                        cmd.Parameters.AddWithValue("@Quantity", l.Quantity);
                        cmd.Parameters.AddWithValue("@LineTotal", l.Price * l.Quantity);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    using (var cmd = new SqlCommand(@"
                        UPDATE Products SET StockQuantity = StockQuantity - @Quantity
                        WHERE ProductId = @ProductId AND StockQuantity >= @Quantity", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", l.Quantity);
                        cmd.Parameters.AddWithValue("@ProductId", l.ProductId);
                        if (await cmd.ExecuteNonQueryAsync() == 0)
                            throw new BusinessException($"'{l.Name}' không đủ hàng trong kho");
                    }
                }

                // 6. Trừ điểm đã dùng + cộng điểm mới tích lũy
                if (request.CustomerId.HasValue)
                {
                    var earned = (int)(total / MoneyPerPoint);
                    using var cmd = new SqlCommand(@"
                        UPDATE Customers SET Points = Points - @Used + @Earned
                        WHERE CustomerId = @CustomerId AND Points >= @Used", conn, transaction);
                    cmd.Parameters.AddWithValue("@Used", request.RedeemPoints);
                    cmd.Parameters.AddWithValue("@Earned", earned);
                    cmd.Parameters.AddWithValue("@CustomerId", request.CustomerId.Value);
                    if (await cmd.ExecuteNonQueryAsync() == 0)
                        throw new BusinessException("Khách hàng không đủ điểm");
                }

                transaction.Commit();
                return orderId;
            }
            catch
            {
                // Hoàn tác toàn bộ: hóa đơn, chi tiết, tồn kho, điểm đều trở về như trước khi thanh toán
                transaction.Rollback();
                throw;
            }
        }

        // ===== Stored Procedure: doanh thu theo ca (theo nhân viên trong khoảng thời gian) =====
        public async Task<List<ShiftRevenueDto>> GetShiftRevenueAsync(DateTime from, DateTime to, int? userId)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.sp_RevenueByShift", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@From", from);
            cmd.Parameters.AddWithValue("@To", to);
            cmd.Parameters.AddWithValue("@UserId", (object?)userId ?? DBNull.Value);
            await conn.OpenAsync();

            var list = new List<ShiftRevenueDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new ShiftRevenueDto
                {
                    UserId = reader.GetInt32(0),
                    StaffName = reader.GetString(1),
                    OrderCount = reader.GetInt32(2),
                    Revenue = reader.GetDecimal(3),
                    TotalDiscount = reader.GetDecimal(4),
                    FirstOrderAt = reader.GetDateTime(5),
                    LastOrderAt = reader.GetDateTime(6)
                });
            }
            return list;
        }

        // ===== Stored Procedure: doanh thu từng ngày (GROUP BY CAST(CreatedAt AS DATE)) =====
        public async Task<List<DailyRevenueDto>> GetDailyRevenueAsync(DateTime fromDate, DateTime toDate)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.sp_DailyRevenue", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate.Date;
            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate.Date;
            await conn.OpenAsync();

            var list = new List<DailyRevenueDto>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new DailyRevenueDto
                {
                    Date = reader.GetDateTime(0),
                    OrderCount = reader.GetInt32(1),
                    Revenue = reader.GetDecimal(2)
                });
            }
            return list;
        }

        // ===== Thẻ KPI: mỗi chỉ số là 1 câu lệnh ExecuteScalar trả về 1 giá trị =====
        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int lowStockThreshold)
        {
            var today = DateTime.Today;
            var monthStart = new DateTime(today.Year, today.Month, 1);

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            async Task<object?> Scalar(string sql, params (string Name, object Value)[] args)
            {
                using var cmd = new SqlCommand(sql, conn);
                foreach (var (name, value) in args) cmd.Parameters.AddWithValue(name, value);
                return await cmd.ExecuteScalarAsync();
            }

            return new DashboardSummaryDto
            {
                TodayRevenue = (decimal)(await Scalar(
                    "SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status = @Status AND CreatedAt >= @From AND CreatedAt < @To",
                    ("@Status", OrderStatus.Completed), ("@From", today), ("@To", today.AddDays(1))))!,
                TodayOrderCount = (int)(await Scalar(
                    "SELECT COUNT(*) FROM Orders WHERE Status = @Status AND CreatedAt >= @From AND CreatedAt < @To",
                    ("@Status", OrderStatus.Completed), ("@From", today), ("@To", today.AddDays(1))))!,
                NewCustomerCount = (int)(await Scalar(
                    "SELECT COUNT(*) FROM Customers WHERE CreatedAt >= @From",
                    ("@From", monthStart)))!,
                LowStockCount = (int)(await Scalar(
                    "SELECT COUNT(*) FROM Products WHERE IsActive = 1 AND StockQuantity <= @Threshold",
                    ("@Threshold", lowStockThreshold)))!
            };
        }
    }
}
