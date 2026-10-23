using System.ComponentModel.DataAnnotations;

namespace ST_BE.Models
{
    public static class OrderStatus
    {
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
    }

    // Hóa đơn bán hàng
    public class Order
    {
        public int OrderId { get; set; }

        [Required, StringLength(30)]
        public string OrderCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }                            // Nhân viên lập hóa đơn
        public User? User { get; set; }

        public int? CustomerId { get; set; }                       // null = khách lẻ
        public Customer? Customer { get; set; }

        public decimal SubTotal { get; set; }

        [Range(0, 1_000_000_000)]
        public decimal Discount { get; set; }

        [Range(0, int.MaxValue)]
        public int PointsUsed { get; set; }                        // Số điểm khách đã dùng cho hóa đơn này

        public decimal PointsDiscount { get; set; }                // Số tiền được trừ nhờ đổi điểm (1 điểm = 1.000đ)

        public decimal TotalAmount { get; set; }

        public decimal CustomerPaid { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }

        public string Status { get; set; } = OrderStatus.Completed;

        public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }
}
