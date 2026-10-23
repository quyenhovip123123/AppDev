using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;
using System.Windows.Forms.DataVisualization.Charting;

namespace ST_FE.Forms
{
    // Tổng quan doanh thu (chỉ Admin)
    public partial class FormDashboard : Form
    {
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

            SetupChart();

            dtpFrom.Value = DateTime.Today.AddDays(-6);
            dtpTo.Value = DateTime.Today;
        }

        private async void FormDashboard_Load(object sender, EventArgs e)
        {
            await LoadSummaryAsync();
            await LoadDataAsync();
        }

        // 4 thẻ KPI: GET /api/reports/dashboard-summary (luôn tính theo hôm nay / tháng này, không phụ thuộc bộ lọc)
        private async Task LoadSummaryAsync()
        {
            try
            {
                var kpi = await ApiClient.GetAsync<DashboardSummaryDto>("reports/dashboard-summary");
                lblTodayRevenue.Text = UiHelper.Money(kpi.TodayRevenue);
                lblTodayOrders.Text = kpi.TodayOrderCount.ToString("N0");
                lblNewCustomers.Text = kpi.NewCustomerCount.ToString("N0");
                lblLowStock.Text = kpi.LowStockCount.ToString("N0");
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        // Biểu đồ cột (System.Windows.Forms.DataVisualization.Charting.Chart, ChartType = Column)
        private void SetupChart()
        {
            chartRevenue.ChartAreas.Clear();
            chartRevenue.Series.Clear();
            chartRevenue.Titles.Clear();
            chartRevenue.Legends.Clear();

            var area = new ChartArea("Revenue");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Format = "dd/MM";
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(229, 231, 235);
            area.AxisY.LabelStyle.Format = "#,##0";
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartRevenue.ChartAreas.Add(area);

            var series = new Series("Doanh thu")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.Date,
                Color = UiHelper.Primary,
                IsValueShownAsLabel = true,
                LabelFormat = "#,##0",
                Font = new Font("Segoe UI", 8F),
                ToolTip = "#VALX{dd/MM/yyyy}: #VALY{#,##0} đ"
            };
            series["PointWidth"] = "0.6";
            chartRevenue.Series.Add(series);

            chartRevenue.Titles.Add(new Title("Doanh thu theo ngày", Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.Black));
            chartRevenue.Titles.Add(new Title("", Docking.Top, new Font("Segoe UI", 9F), Color.DimGray));
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
                var data = await ApiClient.GetAsync<DashboardDto>($"reports/dashboard?from={dtpFrom.Value:yyyy-MM-dd}&to={dtpTo.Value:yyyy-MM-dd}");
                dgvTopProducts.DataSource = data.TopProducts;
                dgvLowStock.DataSource = data.LowStockProducts;

                // Dữ liệu cột lấy từ Stored Procedure sp_DailyRevenue (qua API)
                var series = chartRevenue.Series[0];
                series.Points.Clear();
                foreach (var d in data.DailyRevenue)
                    series.Points.AddXY(d.Date, d.Revenue);
                series.IsValueShownAsLabel = data.DailyRevenue.Count <= 15;
                chartRevenue.ChartAreas[0].AxisX.Interval = Math.Max(1, Math.Ceiling(data.DailyRevenue.Count / 15.0));

                chartRevenue.Titles[0].Text = $"Doanh thu theo ngày ({data.From:dd/MM/yyyy} – {data.To:dd/MM/yyyy})";
                chartRevenue.Titles[1].Text =
                    $"Tổng doanh thu: {UiHelper.Money(data.Revenue)}  •  Lợi nhuận gộp: {UiHelper.Money(data.Profit)}  •  " +
                    $"{data.OrderCount:N0} hóa đơn  •  Tiền nhập hàng: {UiHelper.Money(data.ImportTotal)}";
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnView_Click(object sender, EventArgs e)
        {
            await LoadSummaryAsync();
            await LoadDataAsync();
        }

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
    }
}
