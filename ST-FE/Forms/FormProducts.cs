using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormProducts : Form
    {
        private const int LowStockThreshold = 10;
        private int? _selectedId;

        public FormProducts()
        {
            InitializeComponent();

            var columns = new List<GridColumn>
            {
                new(nameof(ProductDto.ProductCode), "Mã hàng", 70),
                new(nameof(ProductDto.ProductName), "Tên hàng", 200),
                new(nameof(ProductDto.CategoryName), "Nhóm hàng", 110),
                new(nameof(ProductDto.Unit), "ĐVT", 50),
                new(nameof(ProductDto.SalePrice), "Giá bán", 80, "N0"),
                new(nameof(ProductDto.StockQuantity), "Tồn kho", 60, "N0"),
                new(nameof(ProductDto.IsActive), "Đang KD", 55, IsCheckBox: true),
            };
            // Giá nhập chỉ hiển thị cho Quản lý
            if (Session.IsAdmin) columns.Insert(4, new(nameof(ProductDto.CostPrice), "Giá nhập", 80, "N0"));
            UiHelper.SetupGrid(dgvProducts, columns.ToArray());
            dgvProducts.DataBindingComplete += (_, _) => HighlightLowStock();

            UiHelper.StyleButton(btnAdd, UiHelper.Success);
            UiHelper.StyleButton(btnUpdate, UiHelper.Primary);
            UiHelper.StyleButton(btnDelete, UiHelper.Danger);

            if (!Session.IsAdmin)
            {
                btnAdd.Enabled = btnUpdate.Enabled = btnDelete.Enabled = false;
                lblCostPrice.Visible = nudCostPrice.Visible = false;
                grpInfo.Text += " (chỉ xem)";
            }
        }

        private async void FormProducts_Load(object sender, EventArgs e)
        {
            try
            {
                var categories = await ApiClient.GetAsync<List<CategoryDto>>("categories");

                cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategory.ValueMember = nameof(CategoryDto.CategoryId);
                cboCategory.DataSource = categories;

                var filter = new List<CategoryDto> { new() { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" } };
                filter.AddRange(categories);
                cboFilterCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboFilterCategory.ValueMember = nameof(CategoryDto.CategoryId);
                cboFilterCategory.DataSource = filter;
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
            await LoadDataAsync();
        }

        // GET /api/products?keyword=...&categoryId=...&includeInactive=...
        private async Task LoadDataAsync()
        {
            try
            {
                var categoryId = cboFilterCategory.SelectedValue as int? ?? 0;
                var url = $"products?keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}&categoryId={categoryId}&includeInactive={chkShowInactive.Checked}";
                dgvProducts.DataSource = await ApiClient.GetAsync<List<ProductDto>>(url);
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        // Tô đỏ hàng sắp hết, tô xám hàng ngừng kinh doanh
        private void HighlightLowStock()
        {
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.DataBoundItem is not ProductDto p) continue;
                if (!p.IsActive) row.DefaultCellStyle.ForeColor = Color.Gray;
                else if (p.StockQuantity <= LowStockThreshold) row.DefaultCellStyle.ForeColor = UiHelper.Danger;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await LoadDataAsync();

        private async void btnReload_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            cboFilterCategory.SelectedIndex = cboFilterCategory.Items.Count > 0 ? 0 : -1;
            chkShowInactive.Checked = false;
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

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            var p = dgvProducts.SelectedItem<ProductDto>();
            if (p == null) return;

            _selectedId = p.ProductId;
            txtCode.Text = p.ProductCode;
            txtName.Text = p.ProductName;
            cboCategory.SelectedValue = p.CategoryId;
            txtUnit.Text = p.Unit;
            nudCostPrice.Value = Math.Min(p.CostPrice, nudCostPrice.Maximum);
            nudSalePrice.Value = Math.Min(p.SalePrice, nudSalePrice.Maximum);
            nudStock.Value = Math.Min(p.StockQuantity, nudStock.Maximum);
            txtDescription.Text = p.Description ?? string.Empty;
            chkActive.Checked = p.IsActive;
        }

        private bool ValidateInput()
        {
            string? error = null;
            if (string.IsNullOrWhiteSpace(txtCode.Text)) error = "Vui lòng nhập mã hàng!";
            else if (string.IsNullOrWhiteSpace(txtName.Text)) error = "Vui lòng nhập tên hàng!";
            else if (cboCategory.SelectedValue == null) error = "Vui lòng chọn nhóm hàng!";
            else if (string.IsNullOrWhiteSpace(txtUnit.Text)) error = "Vui lòng nhập đơn vị tính!";
            else if (nudSalePrice.Value <= 0) error = "Giá bán phải lớn hơn 0!";

            if (error != null) UiHelper.ShowWarning(error);
            return error == null;
        }

        private object BuildRequest() => new
        {
            ProductCode = txtCode.Text.Trim(),
            ProductName = txtName.Text.Trim(),
            CategoryId = (int)cboCategory.SelectedValue!,
            Unit = txtUnit.Text.Trim(),
            CostPrice = nudCostPrice.Value,
            SalePrice = nudSalePrice.Value,
            StockQuantity = (int)nudStock.Value,
            Description = txtDescription.Text.Trim(),
            IsActive = chkActive.Checked
        };

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PostAsync<ProductDto>("products", BuildRequest());
                UiHelper.ShowInfo("Thêm mặt hàng thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn mặt hàng cần sửa!");
                return;
            }
            if (!ValidateInput()) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PutAsync($"products/{_selectedId}", BuildRequest());
                UiHelper.ShowInfo("Cập nhật thành công!");
                await LoadDataAsync();
            });
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn mặt hàng cần xóa!");
                return;
            }
            if (!UiHelper.Confirm($"Bạn có chắc muốn xóa mặt hàng \"{txtName.Text}\"?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                var message = await ApiClient.DeleteAsync($"products/{_selectedId}");
                UiHelper.ShowInfo(message ?? "Đã xóa mặt hàng");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            dgvProducts.ClearSelection();
            _selectedId = null;
            txtCode.Clear();
            txtName.Clear();
            txtUnit.Clear();
            txtDescription.Clear();
            nudCostPrice.Value = nudSalePrice.Value = nudStock.Value = 0;
            chkActive.Checked = true;
            txtCode.Focus();
        }
    }
}
