/*
 * Mo ta: Chuong trinh Console thuc hanh ADO.NET Buoi 5
 *  - Tiet 1: Bai tap 1 (ExecuteScalar), Bai tap 2 (ExecuteNonQuery + SqlParameter), Bai tap 3 (SqlDataReader)
 *  - Tiet 4: Kiem chung co che Rollback cua SqlTransaction
 */
using System.Text;
using Microsoft.Data.SqlClient;

namespace ST_AdoNetConsole
{
    internal class Program
    {
        // Chuoi ket noi trung voi ST-BE/appsettings.json
        // (Neu dung SQL Server Express, doi Server=localhost thanh Server=.\SQLEXPRESS)
        private const string ConnString =
            "Server=localhost;Database=DBTEST;Trusted_Connection=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== THỰC HÀNH ADO.NET - BUỔI 5 =====");
                Console.WriteLine("1. Bài tập 1: Đếm tổng số mặt hàng (ExecuteScalar)");
                Console.WriteLine("2. Bài tập 2: Cập nhật tồn kho (ExecuteNonQuery + SqlParameter)");
                Console.WriteLine("3. Bài tập 3: Duyệt danh sách sản phẩm (SqlDataReader)");
                Console.WriteLine("4. Tiết 4: Thử nghiệm Rollback với SqlTransaction");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": BaiTap1_ExecuteScalar(); break;
                    case "2": BaiTap2_ExecuteNonQuery(); break;
                    case "3": BaiTap3_SqlDataReader(); break;
                    case "4": Tiet4_TransactionRollback(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            }
        }

        // =====================================================================
        // BAI TAP 1: DEM TONG SO MAT HANG BANG ExecuteScalar()
        // =====================================================================
        static void BaiTap1_ExecuteScalar()
        {
            Console.WriteLine("=== BÀI TẬP 1: ĐẾM TỔNG SỐ LƯỢNG MẶT HÀNG ===");

            // Khoi tao doi tuong SqlConnection trong khoi using de tu dong giai phong
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                try
                {
                    // Cau lenh SQL dem toan bo so dong trong bang Products
                    string query = "SELECT COUNT(*) FROM Products";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Bat buoc phai mo ket noi truoc khi thuc thi
                        connection.Open();
                        Console.WriteLine("-> Đã kết nối thành công tới SQL Server!");

                        // ExecuteScalar() tra ve mot doi tuong kieu object -> ep kieu sang int
                        object? result = command.ExecuteScalar();
                        int totalCount = Convert.ToInt32(result);

                        Console.WriteLine($"-> Tổng số mặt hàng hiện có trong kho siêu thị: {totalCount} sản phẩm.");
                    }
                }
                catch (SqlException ex)
                {
                    // Xu ly loi neu sai chuoi ket noi hoac sai ten bang CSDL
                    Console.WriteLine($"[LỖI SQL]: Không thể truy vấn CSDL. Chi tiết: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LỖI HỆ THỐNG]: {ex.Message}");
                }
            } // Ket thuc using, connection.Close() va connection.Dispose() tu dong duoc goi
        }

        // =====================================================================
        // BAI TAP 2: CAP NHAT TON KHO BANG ExecuteNonQuery() VA SqlParameter
        // =====================================================================
        static void BaiTap2_ExecuteNonQuery()
        {
            Console.WriteLine("=== BÀI TẬP 2: CẬP NHẬT TỒN KHO AN TOÀN VỚI SQLPARAMETER ===");

            // Thong tin do nhan vien nhap tu ban phim
            int maSanPhamCanSua = ReadInt("Nhập mã sản phẩm cần sửa (mặc định 1): ", 1);
            int soLuongTonKhoMoi = ReadInt("Nhập số lượng tồn kho mới (mặc định 250): ", 250);

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                try
                {
                    // Cau lenh UPDATE co 2 tham so @Stock va @Id (Tuyet doi khong cong chuoi)
                    string updateSql = @"UPDATE Products
                                         SET StockQuantity = @Stock
                                         WHERE ProductId = @Id";

                    using (SqlCommand command = new SqlCommand(updateSql, connection))
                    {
                        // Gan gia tri an toan vao tham so thong qua AddWithValue
                        command.Parameters.AddWithValue("@Stock", soLuongTonKhoMoi);
                        command.Parameters.AddWithValue("@Id", maSanPhamCanSua);

                        connection.Open();

                        // ExecuteNonQuery() tra ve so dong bi thay doi trong CSDL
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"-> THÀNH CÔNG: Đã cập nhật tồn kho cho Sản phẩm ID = {maSanPhamCanSua} thành {soLuongTonKhoMoi} đơn vị.");
                            Console.WriteLine($"-> Số dòng dữ liệu bị ảnh hưởng: {rowsAffected} dòng.");
                        }
                        else
                        {
                            Console.WriteLine($"-> CẢNH BÁO: Không tìm thấy sản phẩm có ID = {maSanPhamCanSua} để cập nhật.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"[LỖI SQL]: {ex.Message}");
                }
            }
        }

        // =====================================================================
        // BAI TAP 3: DOC DANH SACH SAN PHAM BANG SqlDataReader
        // =====================================================================
        static void BaiTap3_SqlDataReader()
        {
            Console.WriteLine("=== BÀI TẬP 3: DUYỆT DANH SÁCH BẰNG SQLDATAREADER ===");

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                try
                {
                    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                     FROM Products
                                     ORDER BY ProductId ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Kich hoat luong doc truc tiep bang ExecuteReader()
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            string line = new string('-', 90);
                            Console.WriteLine(line);
                            Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12} | {4,8}",
                                "MÃ SP", "MÃ VẠCH", "TÊN SẢN PHẨM", "ĐƠN GIÁ", "TỒN KHO"));
                            Console.WriteLine(line);

                            // while(reader.Read()): Dich chuyen con tro xuong tung dong du lieu tiep theo
                            int stt = 0;
                            while (reader.Read())
                            {
                                stt++;

                                // Doc gia tri tung cot theo ten cot
                                int id = Convert.ToInt32(reader["ProductId"]);
                                string barcode = reader["Barcode"].ToString() ?? "";
                                string name = reader["ProductName"].ToString() ?? "";
                                decimal price = Convert.ToDecimal(reader["Price"]);
                                int stock = Convert.ToInt32(reader["StockQuantity"]);

                                Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12:N0} đ | {4,8}",
                                    id, barcode, name, price, stock));
                            }

                            Console.WriteLine(line);
                            Console.WriteLine($"-> Tổng cộng đã đọc thành công: {stt} mặt hàng từ SQL Server.");
                        } // Ket thuc using, reader tu dong dong lai
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"[LỖI SQL]: {ex.Message}");
                }
            }
        }

        // =====================================================================
        // TIET 4: KIEM CHUNG CO CHE ROLLBACK CUA SqlTransaction
        // =====================================================================
        static void Tiet4_TransactionRollback()
        {
            Console.WriteLine("=== THỬ NGHIỆM ĐIỀU KHIỂN SQLTRANSACTION ===");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    connection.Open();
                    Console.WriteLine($"-> Tồn kho SP 1 TRƯỚC giao dịch: {GetStock(connection, 1)}");

                    // 1. Khoi tao Transaction
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        Console.WriteLine("-> Bắt đầu phiên giao dịch...");

                        // 2. Thao tac 1: Tru 5 don vi ton kho cua San pham ID = 1 (Hop le)
                        string sqlStep1 = "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @Id";

                        using (SqlCommand cmd1 = new SqlCommand(sqlStep1, connection, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@Qty", 5);
                            cmd1.Parameters.AddWithValue("@Id", 1);
                            int r1 = cmd1.ExecuteNonQuery();
                            Console.WriteLine($"-> Bước 1: Đã trừ tồn kho SP 1 (Số dòng tác động: {r1}).");
                        }

                        // 3. Thao tac 2: Co tinh gay loi de quan sat Rollback
                        Console.WriteLine("-> Bước 2: Chuẩn bị thực thi thao tác lỗi giả lập...");

                        bool isInventoryShortage = true; // Gia lap mat hang thu 2 bi het kho
                        if (isInventoryShortage)
                        {
                            throw new InvalidOperationException("Mặt hàng thứ hai đã hết tồn kho thực tế!");
                        }

                        // 4. Neu khong co loi thi moi Commit
                        transaction.Commit();
                        Console.WriteLine("-> GIAO DỊCH COMMIT THÀNH CÔNG!");
                    }
                    catch (Exception ex)
                    {
                        // 5. Khi bat duoc loi -> Lap tuc Rollback
                        Console.WriteLine($"\n[PHÁT SINH LỖI]: {ex.Message}");
                        transaction.Rollback();
                        Console.WriteLine("-> [HỆ THỐNG]: Đã kích hoạt transaction.Rollback()!");
                        Console.WriteLine("-> Toàn bộ thay đổi của Bước 1 đã được hoàn trả, tồn kho SP 1 không bị trừ!");
                    }

                    Console.WriteLine($"-> Tồn kho SP 1 SAU giao dịch: {GetStock(connection, 1)}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"[LỖI SQL]: {ex.Message}");
            }
        }

        private static int GetStock(SqlConnection connection, int productId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT StockQuantity FROM Products WHERE ProductId = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Id", productId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static int ReadInt(string prompt, int defaultValue)
        {
            Console.Write(prompt);
            return int.TryParse(Console.ReadLine(), out int value) ? value : defaultValue;
        }
    }
}
