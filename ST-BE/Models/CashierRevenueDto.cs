/*
 * Mo ta: DTO hung du lieu ket qua thong ke tu Stored Procedure (Buoi 5 - Tiet 3)
 */
namespace ST_BE.Models
{
    public class CashierRevenueDto
    {
        // Ten dang nhap cua thu ngan chiu trach nhiem ca ban
        public string CashierUsername { get; set; } = string.Empty;

        // Tong so luong don hang thu ngan da xu ly thanh cong
        public int TotalOrders { get; set; }

        // Tong so tien thu ve cho sieu thi (VND)
        public decimal TotalRevenue { get; set; }

        // Tong diem thuong da tich luy cho khach hang
        public int TotalPointsAwarded { get; set; }
    }

    // DTO cho bai tap 3.2: Top 5 san pham ban chay
    public class TopSellingProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
