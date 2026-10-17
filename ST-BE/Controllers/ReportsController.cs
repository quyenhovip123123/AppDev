/*
 * Mo ta: Controller ADO.NET thuc thi Stored Procedure xuat bao cao ca ban (Buoi 5 - Tiet 3)
 * Yeu cau: chay script Database/Buoi5_StoredProcedures.sql tren SSMS truoc khi goi API
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ST_BE.Models;
using System.Data;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Dependency Injection lay chuoi ket noi tu appsettings.json
        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =====================================================================
        // GET: api/reports/revenue-by-cashier
        // Chuc nang: Goi Stored Procedure sp_GetRevenueByCashier qua SqlDataReader
        // =====================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khoi tao SqlConnection trong khoi using dam bao tu dong dong ket noi
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 2. Gan CommandText la CHINH XAC ten Stored Procedure tren SQL Server
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    // 3. DIEM KHOA HOC QUAN TRONG: Thiet lap CommandType la StoredProcedure
                    // Neu thieu dong nay, SQL Server se hieu nham day la chuoi SQL thuong va bao loi cu phap!
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // 4. Mo ket noi vat ly toi SQL Server
                        connection.Open();

                        // 5. Thuc thi bang ExecuteReader(): Nhan ve luong du lieu forward-only
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 6. Vong lap doc tung dong ket qua cua Stored Procedure
                            while (reader.Read())
                            {
                                reportList.Add(MapCashierRevenue(reader));
                            }
                        } // reader tu dong Close() tai day
                    }
                    catch (SqlException ex)
                    {
                        // Bat loi neu quen tao Stored Procedure tren CSDL hoac sai chuoi ket noi
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                        });
                    }
                }
            } // connection tu dong Close() va Dispose() tai day

            // 7. Tra ve ma 200 OK kem danh sach bao cao da tong hop
            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =====================================================================
        // BAI TAP 3.1: GET api/reports/revenue-by-date-range?fromDate=2026-10-01&toDate=2026-10-31
        // Goi Stored Procedure co tham so sp_GetRevenueByDateRange
        // =====================================================================
        [HttpGet("revenue-by-date-range")]
        public IActionResult GetRevenueByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return BadRequest(new { success = false, message = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!" });
            }

            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetRevenueByDateRange", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Truyen tham so cho Stored Procedure (lay tron ngay ket thuc)
                command.Parameters.AddWithValue("@FromDate", fromDate.Date);
                command.Parameters.AddWithValue("@ToDate", toDate.Date.AddDays(1).AddTicks(-1));

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportList.Add(MapCashierRevenue(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                    });
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = fromDate.ToString("yyyy-MM-dd"),
                toDate = toDate.ToString("yyyy-MM-dd"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =====================================================================
        // BAI TAP 3.2: GET api/reports/top-selling-products
        // Goi Stored Procedure sp_GetTop5SellingProducts phuc vu Dashboard Admin
        // =====================================================================
        [HttpGet("top-selling-products")]
        public IActionResult GetTop5SellingProducts()
        {
            var resultList = new List<TopSellingProductDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("sp_GetTop5SellingProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultList.Add(new TopSellingProductDto
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"]),
                                TotalRevenue = reader["TotalRevenue"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["TotalRevenue"])
                                    : 0
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                    });
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // Anh xa tung cot du lieu tu reader vao doi tuong DTO (xu ly an toan DBNull.Value)
        private static CashierRevenueDto MapCashierRevenue(SqlDataReader reader)
        {
            return new CashierRevenueDto
            {
                CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                TotalRevenue = reader["TotalRevenue"] != DBNull.Value
                    ? Convert.ToDecimal(reader["TotalRevenue"])
                    : 0,
                TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                    ? Convert.ToInt32(reader["TotalPointsAwarded"])
                    : 0
            };
        }
    }
}
