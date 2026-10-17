/*
 * Mo ta: Cac lop DTO phuc vu nghiep vu thanh toan gio hang POS (Buoi 5 - Tiet 5)
 */
namespace ST_BE.Models
{
    // DTO bieu dien tung dong san pham trong gio hang
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // DTO nhan yeu cau thanh toan tong the tu may POS
    public class CheckoutRequestDto
    {
        // Tai khoan thu ngan dang dung ca truc
        public string CashierUsername { get; set; } = string.Empty;

        // Ma dinh danh khach hang (khach vang lai: de null hoac 0)
        public int? CustomerId { get; set; }

        // Danh sach cac mon hang can thanh toan
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
