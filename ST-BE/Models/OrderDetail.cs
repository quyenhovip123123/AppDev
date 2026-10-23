using System.ComponentModel.DataAnnotations;

namespace ST_BE.Models
{
    // Chi tiết hóa đơn: một dòng mặt hàng trong hóa đơn
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, StringLength(150)]
        public string ProductName { get; set; } = string.Empty;   // Lưu lại tên tại thời điểm bán

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }                      // Giá vốn tại thời điểm bán (để tính lợi nhuận)

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }
    }
}
