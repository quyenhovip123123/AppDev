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

        public string OrderCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }                            // Nhân viên lập hóa đơn
        public User? User { get; set; }

        public int? CustomerId { get; set; }                       // null = khách lẻ
        public Customer? Customer { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal CustomerPaid { get; set; }

        public string? Note { get; set; }

        public string Status { get; set; } = OrderStatus.Completed;

        public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string ProductName { get; set; } = string.Empty;   // Lưu lại tên tại thời điểm bán

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }                      // Giá vốn tại thời điểm bán (để tính lợi nhuận)

        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }
    }
}
