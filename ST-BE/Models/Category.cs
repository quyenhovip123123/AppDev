namespace ST_BE.Models
{
    // Nhóm hàng (VD: Bút viết, Vở & Sổ, Giấy in...)
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
