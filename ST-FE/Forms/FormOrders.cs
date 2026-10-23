using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    // Danh sách hóa đơn. Nhân viên chỉ thấy hóa đơn của mình (server lọc theo UserId trong JWT)
    public partial class FormOrders : Form
    {
        private readonly PagerBar _pager = new("hóa đơn");

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
                new GridColumn(nameof(OrderDto.PointsDiscount), "Trừ điểm", 70, "N0"),
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
            UiHelper.StyleButton(btnExport, UiHelper.Success);
            if (!Session.IsAdmin) UiHelper.StyleLocked(btnCancelOrder);

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            _pager.AttachBelow(dgvOrders).PageChanged += async (_, _) => await LoadDataAsync();
        }

        private string FilterQuery() =>
            $"from={dtpFrom.Value:yyyy-MM-dd}&to={dtpTo.Value:yyyy-MM-dd}&keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}";

        private async void FormOrders_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // GET /api/orders/paged?from=...&to=...&keyword=...&page=...&pageSize=... (phân trang phía máy chủ)
        private async Task LoadDataAsync()
        {
            try
            {
                var result = await ApiClient.GetAsync<OrderPagedResult>($"orders/paged?{FilterQuery()}&{_pager.Query}");
                if (!_pager.Apply(result))
                {
                    await LoadDataAsync();
                    return;
                }
                dgvOrders.DataSource = result.Items;

                // Tổng hợp do server tính trên toàn bộ kết quả lọc, không chỉ trang đang xem
                lblSummary.Text = $"{result.CompletedCount} hóa đơn hoàn thành  •  Doanh thu: {UiHelper.Money(result.Revenue)}";
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        // Xuất toàn bộ hóa đơn theo bộ lọc (không chỉ trang đang xem) ra tệp CSV mở bằng Excel
        private async void btnExport_Click(object sender, EventArgs e)
        {
            await UiHelper.RunBusyAsync(this, async () =>
            {
                var orders = await ApiClient.GetAsync<List<OrderDto>>($"orders?{FilterQuery()}");
                if (orders.Count == 0)
                {
                    UiHelper.ShowWarning("Không có hóa đơn nào để xuất!");
                    return;
                }
                var saved = CsvExporter.SaveWithDialog(this, $"DoanhThu_{dtpFrom.Value:yyyyMMdd}-{dtpTo.Value:yyyyMMdd}.csv", orders,
                    new("Mã hóa đơn", o => o.OrderCode),
                    new("Ngày lập", o => o.CreatedAt),
                    new("Nhân viên", o => o.StaffName),
                    new("Khách hàng", o => o.CustomerName),
                    new("Tiền hàng", o => o.SubTotal),
                    new("Giảm giá", o => o.Discount),
                    new("Điểm đã dùng", o => o.PointsUsed),
                    new("Trừ tiền điểm", o => o.PointsDiscount),
                    new("Thành tiền", o => o.TotalAmount),
                    new("Trạng thái", o => o.StatusText));
                if (saved) UiHelper.ShowInfo($"Đã xuất {orders.Count} hóa đơn ra tệp CSV.");
            });
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _pager.Reset();
            await LoadDataAsync();
        }

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
            if (!UiHelper.RequireAdmin("hủy hóa đơn")) return;
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
