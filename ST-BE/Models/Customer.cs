namespace ST_BE.Models
{
    // Khách hàng thân thiết
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public int Points { get; set; }                            // Điểm tích lũy: 10.000đ = 1 điểm

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
