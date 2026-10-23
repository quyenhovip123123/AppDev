using System.ComponentModel.DataAnnotations;

namespace ST_BE.Models
{
    // Khách hàng thân thiết
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc"), StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc"), StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [Range(0, int.MaxValue)]
        public int Points { get; set; }                            // Điểm tích lũy: 10.000đ = 1 điểm

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
