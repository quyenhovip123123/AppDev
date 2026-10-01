using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;
using System.Drawing.Drawing2D;

namespace ST_FE.Forms
{
    // Tổng quan doanh thu (chỉ Admin)
    public partial class FormDashboard : Form
    {
        private DashboardDto? _data;

        public FormDashboard()
        {
            InitializeComponent();
            UiHelper.SetupGrid(dgvTopProducts,
                new GridColumn(nameof(TopProductDto.ProductCode), "Mã", 60),
                new GridColumn(nameof(TopProductDto.ProductName), "Tên hàng", 180),
                new GridColumn(nameof(TopProductDto.QuantitySold), "Đã bán", 60, "N0"),
                new GridColumn(nameof(TopProductDto.Revenue), "Doanh thu", 90, "N0"));
            UiHelper.SetupGrid(dgvLowStock,
                new GridColumn(nameof(ProductDto.ProductCode), "Mã", 60),
                new GridColumn(nameof(ProductDto.ProductName), "Tên hàng", 180),
                new GridColumn(nameof(ProductDto.CategoryName), "Nhóm", 100),
                new GridColumn(nameof(ProductDto.StockQuantity), "Tồn", 50, "N0"));
            UiHelper.StyleButton(btnView, UiHelper.Primary);

            // Vẽ biểu đồ mượt, không nháy
            typeof(Panel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(pnlChart, true);

            dtpFrom.Value = DateTime.Today.AddDays(-6);
            dtpTo.Value = DateTime.Today;
        }

        private async void FormDashboard_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                UiHelper.ShowWarning("Từ ngày phải nhỏ hơn hoặc bằng đến ngày!");
                return;
            }
            try
            {
                _data = await ApiClient.GetAsync<DashboardDto>($"reports/dashboard?from={dtpFrom.Value:yyyy-MM-dd}&to={dtpTo.Value:yyyy-MM-dd}");
                lblRevenue.Text = UiHelper.Money(_data.Revenue);
                lblProfit.Text = UiHelper.Money(_data.Profit);
                lblOrders.Text = _data.OrderCount.ToString("N0");
                lblImport.Text = UiHelper.Money(_data.ImportTotal);
                dgvTopProducts.DataSource = _data.TopProducts;
                dgvLowStock.DataSource = _data.LowStockProducts;
                pnlChart.Invalidate();
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnView_Click(object sender, EventArgs e) => await LoadDataAsync();

        private async void btnToday_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = dtpTo.Value = DateTime.Today;
            await LoadDataAsync();
        }

        private async void btn7Days_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-6);
            dtpTo.Value = DateTime.Today;
            await LoadDataAsync();
        }

        private async void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
            await LoadDataAsync();
        }

        private void pnlChart_Resize(object sender, EventArgs e) => pnlChart.Invalidate();

        // Biểu đồ cột doanh thu theo ngày (tự vẽ bằng GDI+)
        private void pnlChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var titleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            using var smallFont = new Font("Segoe UI", 8);
            g.DrawString("Doanh thu theo ngày", titleFont, Brushes.Black, 12, 8);

            var days = _data?.DailyRevenue;
            if (days == null || days.Count == 0) return;

            var area = new RectangleF(60, 40, pnlChart.Width - 80, pnlChart.Height - 75);
            var max = Math.Max(days.Max(d => d.Revenue), 1);

            // Trục và đường kẻ ngang
            using var gridPen = new Pen(Color.FromArgb(229, 231, 235));
            for (int i = 0; i <= 4; i++)
            {
                var y = area.Bottom - area.Height * i / 4;
                g.DrawLine(gridPen, area.Left, y, area.Right, y);
                var label = (max * i / 4 / 1000).ToString("N0") + "k";
                g.DrawString(label, smallFont, Brushes.Gray, new RectangleF(0, y - 7, area.Left - 6, 14),
                    new StringFormat { Alignment = StringAlignment.Far });
            }

            var slot = area.Width / days.Count;
            var barWidth = Math.Min(slot * 0.6f, 50);
            var labelEvery = Math.Max(1, (int)Math.Ceiling(days.Count / (area.Width / 45)));
            using var barBrush = new SolidBrush(UiHelper.Primary);
            var center = new StringFormat { Alignment = StringAlignment.Center };

            for (int i = 0; i < days.Count; i++)
            {
                var d = days[i];
                var h = (float)(d.Revenue / max) * area.Height;
                var x = area.Left + slot * i + (slot - barWidth) / 2;
                if (h > 0) g.FillRectangle(barBrush, x, area.Bottom - h, barWidth, h);

                if (i % labelEvery == 0)
                    g.DrawString(d.Date.ToString("dd/MM"), smallFont, Brushes.Gray,
                        new RectangleF(area.Left + slot * i, area.Bottom + 4, slot, 14), center);
                if (d.Revenue > 0 && days.Count <= 15)
                    g.DrawString((d.Revenue / 1000).ToString("N0") + "k", smallFont, Brushes.Black,
                        new RectangleF(area.Left + slot * i, area.Bottom - h - 16, slot, 14), center);
            }
        }
    }
}
