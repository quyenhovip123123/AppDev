using System.ComponentModel.DataAnnotations;

namespace ST_BE.Models
{
    // Mặt hàng văn phòng phẩm
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã hàng là bắt buộc"), StringLength(30)]
        public string ProductCode { get; set; } = string.Empty;   // Mã hàng nội bộ (VD: BUT001)

        [StringLength(20)]
        public string? Barcode { get; set; }                      // Mã vạch in trên bao bì (EAN-13), null = chưa có

        [Required(ErrorMessage = "Tên hàng là bắt buộc"), StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [StringLength(20)]
        public string Unit { get; set; } = "Cái";                 // Đơn vị tính: Cây, Quyển, Ram, Hộp...

        [Range(0, 1_000_000_000, ErrorMessage = "Giá nhập không được âm")]
        public decimal CostPrice { get; set; }                     // Giá nhập gần nhất

        // Đọc "0.01" theo InvariantCulture: máy đặt định dạng Việt Nam (dấu phẩy thập phân) vẫn không lỗi
        [Range(typeof(decimal), "0.01", "1000000000", ErrorMessage = "Giá bán phải lớn hơn 0",
            ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public decimal SalePrice { get; set; }                     // Giá bán

        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho không được âm")]
        public int StockQuantity { get; set; }                     // Tồn kho

        [StringLength(255)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;                 // false = ngừng kinh doanh
    }
}
