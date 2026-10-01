namespace ST_BE.Models
{
    // Mặt hàng văn phòng phẩm
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = string.Empty;   // Mã hàng / mã vạch

        public string ProductName { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string Unit { get; set; } = "Cái";                 // Đơn vị tính: Cây, Quyển, Ram, Hộp...

        public decimal CostPrice { get; set; }                     // Giá nhập gần nhất

        public decimal SalePrice { get; set; }                     // Giá bán

        public int StockQuantity { get; set; }                     // Tồn kho

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;                 // false = ngừng kinh doanh
    }
}
