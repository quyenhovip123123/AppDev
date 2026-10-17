using ST_FE.WinForms;
using System;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace ST_FE
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today;
            await RunReportAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await RunReportAsync();
        }

        private async Task RunReportAsync()
        {
            // Lớp bảo vệ cấp Action: chỉ Admin mới được xem dữ liệu tài chính
            if (SessionManager.CurrentRole != "Admin")
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                btnRunReport.Enabled = false;
                string date = dtpReportDate.Value.ToString("yyyy-MM-dd");
                var report = await ApiClientService.Client.GetFromJsonAsync<QuickReportDto>($"orders/report?date={date}");
                if (report == null)
                    return;

                lblTotalOrders.Text = report.TotalOrders.ToString("N0");
                lblTotalRevenue.Text = $"{report.TotalRevenue:N0} đ";
                lblBestSeller.Text = string.IsNullOrEmpty(report.BestSellerName)
                    ? "Chưa có giao dịch"
                    : $"{report.BestSellerName}\n({report.BestSellerQuantity} sp)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = true;
            }
        }
    }

    public class QuickReportDto
    {
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public string BestSellerName { get; set; } = string.Empty;
        public int BestSellerQuantity { get; set; }
    }
}
