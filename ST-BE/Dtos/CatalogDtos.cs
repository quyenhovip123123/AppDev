using System.ComponentModel.DataAnnotations;

namespace ST_BE.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProductCount { get; set; }
    }

    public class CategoryRequest
    {
        [Required(ErrorMessage = "Tên nhóm hàng không được trống"), StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductRequest
    {
        [Required(ErrorMessage = "Mã hàng không được trống"), StringLength(30)]
        public string ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên hàng không được trống"), StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhóm hàng")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Đơn vị tính không được trống"), StringLength(20)]
        public string Unit { get; set; } = string.Empty;

        [Range(0, 1_000_000_000, ErrorMessage = "Giá nhập không hợp lệ")]
        public decimal CostPrice { get; set; }

        [Range(0, 1_000_000_000, ErrorMessage = "Giá bán không hợp lệ")]
        public decimal SalePrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho không hợp lệ")]
        public int StockQuantity { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerRequest
    {
        [Required(ErrorMessage = "Họ tên không được trống"), StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được trống")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 0 và có 10-11 chữ số")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ"), StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }
    }
}
