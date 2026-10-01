using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;
using System.ComponentModel;

namespace ST_FE.Forms
{
    // Nhập kho (chỉ Admin)
    public partial class FormImports : Form
    {
        private readonly BindingList<CartItem> _lines = new();

        public FormImports()
        {
            InitializeComponent();

            UiHelper.SetupGrid(dgvLines,
                new GridColumn(nameof(CartItem.ProductCode), "Mã hàng", 70),
                new GridColumn(nameof(CartItem.ProductName), "Tên hàng", 220),
                new GridColumn(nameof(CartItem.Unit), "ĐVT", 50),
                new GridColumn(nameof(CartItem.Quantity), "Số lượng", 70, "N0"),
                new GridColumn(nameof(CartItem.UnitPrice), "Giá nhập", 90, "N0"),
                new GridColumn(nameof(CartItem.LineTotal), "Thành tiền", 100, "N0"));
            dgvLines.DataSource = _lines;

            UiHelper.SetupGrid(dgvReceipts,
                new GridColumn(nameof(ImportReceiptDto.ReceiptCode), "Mã phiếu", 120),
                new GridColumn(nameof(ImportReceiptDto.CreatedAt), "Ngày nhập", 110, "dd/MM/yyyy HH:mm"),
                new GridColumn(nameof(ImportReceiptDto.SupplierName), "Nhà cung cấp", 160),
                new GridColumn(nameof(ImportReceiptDto.StaffName), "Người lập", 130),
                new GridColumn(nameof(ImportReceiptDto.TotalAmount), "Tổng tiền", 100, "N0"),
                new GridColumn(nameof(ImportReceiptDto.Note), "Ghi chú", 150));

            UiHelper.SetupGrid(dgvReceiptDetails,
                new GridColumn(nameof(ImportDetailDto.ProductCode), "Mã hàng", 70),
                new GridColumn(nameof(ImportDetailDto.ProductName), "Tên hàng", 220),
                new GridColumn(nameof(ImportDetailDto.Quantity), "Số lượng", 70, "N0"),
                new GridColumn(nameof(ImportDetailDto.UnitCost), "Giá nhập", 90, "N0"),
                new GridColumn(nameof(ImportDetailDto.LineTotal), "Thành tiền", 100, "N0"));

            UiHelper.StyleButton(btnAddLine, UiHelper.Primary);
            UiHelper.StyleButton(btnRemoveLine, UiHelper.Danger);
            UiHelper.StyleButton(btnSaveImport, UiHelper.Success);

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
        }

        private async void FormImports_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
            await LoadHistoryAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var products = await ApiClient.GetAsync<List<ProductDto>>("products");
                cboProduct.DisplayMember = nameof(ProductDto.ProductName);
                cboProduct.ValueMember = nameof(ProductDto.ProductId);
                cboProduct.DataSource = products;
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        // Chọn mặt hàng -> gợi ý giá nhập gần nhất
        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProduct.SelectedItem is ProductDto p)
                nudUnitCost.Value = Math.Min(p.CostPrice, nudUnitCost.Maximum);
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            if (cboProduct.SelectedItem is not ProductDto p)
            {
                UiHelper.ShowWarning("Vui lòng chọn mặt hàng!");
                return;
            }

            var line = _lines.FirstOrDefault(l => l.ProductId == p.ProductId);
            if (line == null)
            {
                _lines.Add(new CartItem
                {
                    ProductId = p.ProductId,
                    ProductCode = p.ProductCode,
                    ProductName = p.ProductName,
                    Unit = p.Unit,
                    Quantity = (int)nudImportQty.Value,
                    UnitPrice = nudUnitCost.Value
                });
            }
            else
            {
                line.Quantity += (int)nudImportQty.Value;
                line.UnitPrice = nudUnitCost.Value;
                _lines.ResetBindings();
            }
            nudImportQty.Value = 1;
            UpdateTotal();
        }

        private void btnRemoveLine_Click(object sender, EventArgs e)
        {
            var line = dgvLines.SelectedItem<CartItem>();
            if (line == null) return;
            _lines.Remove(line);
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            lblImportTotal.Text = "Tổng tiền: " + UiHelper.Money(_lines.Sum(l => l.LineTotal));
        }

        private async void btnSaveImport_Click(object sender, EventArgs e)
        {
            if (_lines.Count == 0)
            {
                UiHelper.ShowWarning("Phiếu nhập chưa có mặt hàng nào!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSupplier.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập tên nhà cung cấp!");
                txtSupplier.Focus();
                return;
            }
            if (!UiHelper.Confirm($"Lưu phiếu nhập {_lines.Count} mặt hàng, tổng {UiHelper.Money(_lines.Sum(l => l.LineTotal))}?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                var receipt = await ApiClient.PostAsync<ImportReceiptDto>("imports", new
                {
                    SupplierName = txtSupplier.Text.Trim(),
                    Note = txtImportNote.Text.Trim(),
                    Items = _lines.Select(l => new { l.ProductId, l.Quantity, UnitCost = l.UnitPrice }).ToList()
                });
                UiHelper.ShowInfo($"Đã lưu phiếu nhập {receipt.ReceiptCode}. Tồn kho đã được cập nhật.");

                _lines.Clear();
                txtSupplier.Clear();
                txtImportNote.Clear();
                UpdateTotal();
                await LoadProductsAsync();
                await LoadHistoryAsync();
            });
        }

        // ===== Lịch sử =====

        private async Task LoadHistoryAsync()
        {
            try
            {
                dgvReceipts.DataSource = await ApiClient.GetAsync<List<ImportReceiptDto>>(
                    $"imports?from={dtpFrom.Value:yyyy-MM-dd}&to={dtpTo.Value:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnLoadHistory_Click(object sender, EventArgs e) => await LoadHistoryAsync();

        private void dgvReceipts_SelectionChanged(object sender, EventArgs e)
        {
            dgvReceiptDetails.DataSource = dgvReceipts.SelectedItem<ImportReceiptDto>()?.Details;
        }
    }
}
