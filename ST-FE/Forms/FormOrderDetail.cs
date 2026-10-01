using ST_FE.Helpers;
using ST_FE.Models;
using System.Drawing.Printing;

namespace ST_FE.Forms
{
    // Xem chi tiết và in hóa đơn
    public partial class FormOrderDetail : Form
    {
        private readonly OrderDto _order;

        public FormOrderDetail(OrderDto order)
        {
            InitializeComponent();
            _order = order;

            UiHelper.SetupGrid(dgvDetails,
                new GridColumn(nameof(OrderDetailDto.ProductCode), "Mã hàng", 70),
                new GridColumn(nameof(OrderDetailDto.ProductName), "Tên hàng", 200),
                new GridColumn(nameof(OrderDetailDto.Quantity), "SL", 40, "N0"),
                new GridColumn(nameof(OrderDetailDto.UnitPrice), "Đơn giá", 80, "N0"),
                new GridColumn(nameof(OrderDetailDto.LineTotal), "Thành tiền", 90, "N0"));
            UiHelper.StyleButton(btnPrint, UiHelper.Primary);
        }

        private void FormOrderDetail_Load(object sender, EventArgs e)
        {
            Text = "Hóa đơn " + _order.OrderCode;
            lblOrderCode.Text = $"HÓA ĐƠN {_order.OrderCode}";
            if (_order.Status == "Cancelled")
            {
                lblOrderCode.Text += "  (ĐÃ HỦY)";
                lblOrderCode.ForeColor = UiHelper.Danger;
            }
            lblInfoLeft.Text = $"Ngày lập: {_order.CreatedAt:dd/MM/yyyy HH:mm}\nNhân viên: {_order.StaffName}";
            lblInfoRight.Text = $"Khách hàng: {_order.CustomerName}\nTrạng thái: {_order.StatusText}";
            lblNote.Text = string.IsNullOrWhiteSpace(_order.Note) ? "" : "Ghi chú: " + _order.Note;
            lblTotals.Text =
                $"Tổng tiền hàng: {UiHelper.Money(_order.SubTotal)}\n" +
                $"Giảm giá: {UiHelper.Money(_order.Discount)}\n" +
                $"KHÁCH CẦN TRẢ: {UiHelper.Money(_order.TotalAmount)}\n" +
                $"Khách đưa: {UiHelper.Money(_order.CustomerPaid)}  •  Tiền thừa: {UiHelper.Money(_order.ChangeAmount)}";
            dgvDetails.DataSource = _order.Details;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using var doc = new PrintDocument { DocumentName = _order.OrderCode };
            doc.PrintPage += PrintReceipt;
            using var preview = new PrintPreviewDialog { Document = doc, Width = 600, Height = 800 };
            preview.ShowDialog(this);
        }

        // Vẽ hóa đơn dạng phiếu tính tiền
        private void PrintReceipt(object? sender, PrintPageEventArgs e)
        {
            var g = e.Graphics!;
            using var title = new Font("Segoe UI", 16, FontStyle.Bold);
            using var bold = new Font("Segoe UI", 10, FontStyle.Bold);
            using var normal = new Font("Segoe UI", 10);
            float left = e.MarginBounds.Left, right = e.MarginBounds.Right, y = e.MarginBounds.Top;
            var center = new StringFormat { Alignment = StringAlignment.Center };
            var alignRight = new StringFormat { Alignment = StringAlignment.Far };
            var width = right - left;

            g.DrawString("CỬA HÀNG VĂN PHÒNG PHẨM SUNNY", title, Brushes.Black, new RectangleF(left, y, width, 30), center); y += 34;
            g.DrawString("HÓA ĐƠN BÁN HÀNG", bold, Brushes.Black, new RectangleF(left, y, width, 20), center); y += 30;
            g.DrawString($"Số HĐ: {_order.OrderCode}", normal, Brushes.Black, left, y); y += 20;
            g.DrawString($"Ngày: {_order.CreatedAt:dd/MM/yyyy HH:mm}", normal, Brushes.Black, left, y); y += 20;
            g.DrawString($"Thu ngân: {_order.StaffName}", normal, Brushes.Black, left, y); y += 20;
            g.DrawString($"Khách hàng: {_order.CustomerName}", normal, Brushes.Black, left, y); y += 28;

            g.DrawLine(Pens.Black, left, y, right, y); y += 6;
            g.DrawString("Mặt hàng", bold, Brushes.Black, left, y);
            g.DrawString("SL", bold, Brushes.Black, left + width * 0.55f, y);
            g.DrawString("Đơn giá", bold, Brushes.Black, left + width * 0.65f, y);
            g.DrawString("Thành tiền", bold, Brushes.Black, new RectangleF(left, y, width, 20), alignRight); y += 24;

            foreach (var d in _order.Details)
            {
                g.DrawString(d.ProductName, normal, Brushes.Black, new RectangleF(left, y, width * 0.54f, 20));
                g.DrawString(d.Quantity.ToString("N0"), normal, Brushes.Black, left + width * 0.55f, y);
                g.DrawString(d.UnitPrice.ToString("N0"), normal, Brushes.Black, left + width * 0.65f, y);
                g.DrawString(d.LineTotal.ToString("N0"), normal, Brushes.Black, new RectangleF(left, y, width, 20), alignRight);
                y += 22;
            }
            g.DrawLine(Pens.Black, left, y, right, y); y += 8;

            void Line(string caption, decimal value, Font font)
            {
                g.DrawString(caption, font, Brushes.Black, left + width * 0.45f, y);
                g.DrawString(UiHelper.Money(value), font, Brushes.Black, new RectangleF(left, y, width, 20), alignRight);
                y += 22;
            }
            Line("Tổng tiền hàng:", _order.SubTotal, normal);
            Line("Giảm giá:", _order.Discount, normal);
            Line("Khách cần trả:", _order.TotalAmount, bold);
            Line("Khách đưa:", _order.CustomerPaid, normal);
            Line("Tiền thừa:", _order.ChangeAmount, normal);
            y += 20;
            g.DrawString("Cảm ơn quý khách và hẹn gặp lại!", normal, Brushes.Black, new RectangleF(left, y, width, 20), center);
        }
    }
}
