using System.ComponentModel.DataAnnotations;

namespace ST_BE.Dtos
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }

        [Range(1, 100000, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
    }

    public class CreateOrderRequest
    {
        public int? CustomerId { get; set; }

        [Range(0, 1_000_000_000, ErrorMessage = "Giảm giá không hợp lệ")]
        public decimal Discount { get; set; }

        [Range(0, 10_000_000_000, ErrorMessage = "Tiền khách đưa không hợp lệ")]
        public decimal CustomerPaid { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }

        [MinLength(1, ErrorMessage = "Hóa đơn phải có ít nhất 1 mặt hàng")]
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CustomerPaid { get; set; }
        public decimal ChangeAmount { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderDetailDto> Details { get; set; } = new();
    }

    public class ImportItemRequest
    {
        public int ProductId { get; set; }

        [Range(1, 1_000_000, ErrorMessage = "Số lượng nhập phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Range(0, 1_000_000_000, ErrorMessage = "Giá nhập không hợp lệ")]
        public decimal UnitCost { get; set; }
    }

    public class CreateImportRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập nhà cung cấp"), StringLength(150)]
        public string SupplierName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Note { get; set; }

        [MinLength(1, ErrorMessage = "Phiếu nhập phải có ít nhất 1 mặt hàng")]
        public List<ImportItemRequest> Items { get; set; } = new();
    }

    public class ImportDetailDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class ImportReceiptDto
    {
        public int ImportReceiptId { get; set; }
        public string ReceiptCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string? Note { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ImportDetailDto> Details { get; set; } = new();
    }

    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public int OrderCount { get; set; }
        public decimal Revenue { get; set; }
    }

    public class TopProductDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DashboardDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int OrderCount { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }            // Doanh thu - giá vốn
        public decimal ImportTotal { get; set; }
        public int ProductCount { get; set; }
        public int CustomerCount { get; set; }
        public List<DailyRevenueDto> DailyRevenue { get; set; } = new();
        public List<TopProductDto> TopProducts { get; set; } = new();
        public List<ProductDto> LowStockProducts { get; set; } = new();
    }
}
