using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    // Danh sách hóa đơn. Nhân viên chỉ thấy hóa đơn của mình (server lọc theo UserId trong JWT)
    public partial class FormOrders : Form
    {
        public FormOrders()
        {
            InitializeComponent();
            UiHelper.SetupGrid(dgvOrders,
                new GridColumn(nameof(OrderDto.OrderCode), "Mã hóa đơn", 120),
                new GridColumn(nameof(OrderDto.CreatedAt), "Ngày lập", 110, "dd/MM/yyyy HH:mm"),
                new GridColumn(nameof(OrderDto.StaffName), "Nhân viên", 130),
                new GridColumn(nameof(OrderDto.CustomerName), "Khách hàng", 130),
                new GridColumn(nameof(OrderDto.SubTotal), "Tiền hàng", 90, "N0"),
                new GridColumn(nameof(OrderDto.Discount), "Giảm giá", 80, "N0"),
                new GridColumn(nameof(OrderDto.TotalAmount), "Thành tiền", 90, "N0"),
                new GridColumn(nameof(OrderDto.StatusText), "Trạng thái", 80));
            dgvOrders.DataBindingComplete += (_, _) =>
            {
                foreach (DataGridViewRow row in dgvOrders.Rows)
                    if (row.DataBoundItem is OrderDto { Status: "Cancelled" })
                        row.DefaultCellStyle.ForeColor = Color.Gray;
            };

            UiHelper.StyleButton(btnViewDetail, UiHelper.Primary);
            UiHelper.StyleButton(btnCancelOrder, UiHelper.Danger);
            btnCancelOrder.Visible = Session.IsAdmin;

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
        }

        private async void FormOrders_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var url = $"orders?from={dtpFrom.Value:yyyy-MM-dd}&to={dtpTo.Value:yyyy-MM-dd}&keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}";
                var orders = await ApiClient.GetAsync<List<OrderDto>>(url);
                dgvOrders.DataSource = orders;

                var completed = orders.Where(o => o.Status != "Cancelled").ToList();
                lblSummary.Text = $"{completed.Count} hóa đơn hoàn thành  •  Doanh thu: {UiHelper.Money(completed.Sum(o => o.TotalAmount))}";
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await LoadDataAsync();

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnViewDetail.PerformClick();
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            var order = dgvOrders.SelectedItem<OrderDto>();
            if (order == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn hóa đơn!");
                return;
            }
            using var f = new FormOrderDetail(order);
            f.ShowDialog(this);
        }

        private async void btnCancelOrder_Click(object sender, EventArgs e)
        {
            var order = dgvOrders.SelectedItem<OrderDto>();
            if (order == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn hóa đơn cần hủy!");
                return;
            }
            if (order.Status == "Cancelled")
            {
                UiHelper.ShowWarning("Hóa đơn này đã bị hủy!");
                return;
            }
            if (!UiHelper.Confirm($"Hủy hóa đơn {order.OrderCode}?\nHàng sẽ được hoàn lại kho và trừ điểm khách hàng.")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                var message = await ApiClient.PostAsync($"orders/{order.OrderId}/cancel");
                UiHelper.ShowInfo(message ?? "Đã hủy hóa đơn");
                await LoadDataAsync();
            });
        }
    }
}
