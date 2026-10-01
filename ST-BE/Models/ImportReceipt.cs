namespace ST_BE.Models
{
    // Phiếu nhập kho
    public class ImportReceipt
    {
        public int ImportReceiptId { get; set; }

        public string ReceiptCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User? User { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public string? Note { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<ImportReceiptDetail> Details { get; set; } = new List<ImportReceiptDetail>();
    }

    public class ImportReceiptDetail
    {
        public int Id { get; set; }

        public int ImportReceiptId { get; set; }
        public ImportReceipt? ImportReceipt { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public decimal LineTotal { get; set; }
    }
}
