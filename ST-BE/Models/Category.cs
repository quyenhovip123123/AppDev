using System.ComponentModel.DataAnnotations;

namespace ST_BE.Models
{
    // Nhóm hàng (VD: Bút viết, Vở & Sổ, Giấy in...)
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên nhóm hàng là bắt buộc"), StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
