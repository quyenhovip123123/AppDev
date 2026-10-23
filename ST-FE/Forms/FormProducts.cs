using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormProducts : Form
    {
        private const int LowStockThreshold = 10;
        private const int PageSize = 15;
        private int? _selectedId;
        private int _page = 1;
        private int _totalPages = 1;

        public FormProducts()
        {
            InitializeComponent();

            var columns = new List<GridColumn>
            {
                new(nameof(ProductDto.ProductCode), "Mã hàng", 70),
                new(nameof(ProductDto.Barcode), "Mã vạch", 105),
                new(nameof(ProductDto.ProductName), "Tên hàng", 200),
                new(nameof(ProductDto.CategoryName), "Nhóm hàng", 110),
                new(nameof(ProductDto.Unit), "ĐVT", 50),
                new(nameof(ProductDto.SalePrice), "Giá bán", 80, "N0"),
                new(nameof(ProductDto.StockQuantity), "Tồn kho", 60, "N0"),
                new(nameof(ProductDto.IsActive), "Đang KD", 55, IsCheckBox: true),
            };
            // Giá nhập chỉ hiển thị cho Quản lý
            if (Session.IsAdmin) columns.Insert(5, new(nameof(ProductDto.CostPrice), "Giá nhập", 80, "N0"));
            UiHelper.SetupGrid(dgvProducts, columns.ToArray());
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;

            UiHelper.StyleButton(btnAdd, UiHelper.Success);
            UiHelper.StyleButton(btnUpdate, UiHelper.Primary);
            UiHelper.StyleButton(btnDelete, UiHelper.Danger);
            UiHelper.StyleButton(btnExport, UiHelper.Success);

            // Nhân viên chỉ được xem: các nút vẫn bấm được nhưng sẽ hiện hộp thoại "Không đủ quyền hạn"
            if (!Session.IsAdmin)
            {
                foreach (var btn in new[] { btnAdd, btnUpdate, btnDelete, btnExport }) UiHelper.StyleLocked(btn);
                lblCostPrice.Visible = nudCostPrice.Visible = false;
                grpInfo.Text += " (chỉ xem)";
            }
        }

        private async void FormProducts_Load(object sender, EventArgs e)
        {
            // Sau khi tự co giãn theo font (VD chạy trên Wine thiếu Segoe UI), nhãn trang có thể đè lên nút "Sau ▶"
            btnNextPage.Left = lblPage.Right + (lblPage.Left - btnPrevPage.Right);

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

        private string FilterQuery()
        {
            var categoryId = cboFilterCategory.SelectedValue as int? ?? 0;
            return $"keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}&categoryId={categoryId}&includeInactive={chkShowInactive.Checked}";
        }

        // GET /api/products/paged?keyword=...&categoryId=...&page=...&pageSize=... (phân trang phía máy chủ)
        private async Task LoadDataAsync()
        {
            try
            {
                var result = await ApiClient.GetAsync<PagedResult<ProductDto>>($"products/paged?{FilterQuery()}&page={_page}&pageSize={PageSize}");
                // Trang hiện tại vượt quá số trang (VD vừa xóa mặt hàng cuối của trang cuối) -> lùi về trang cuối
                if (result.Items.Count == 0 && result.TotalPages > 0 && _page > result.TotalPages)
                {
                    _page = result.TotalPages;
                    await LoadDataAsync();
                    return;
                }

                dgvProducts.DataSource = result.Items;
                _totalPages = Math.Max(1, result.TotalPages);
                lblPage.Text = $"Trang {_page}/{_totalPages}  •  {result.TotalCount} mặt hàng";
                btnPrevPage.Enabled = _page > 1;
                btnNextPage.Enabled = _page < _totalPages;
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        // Cảnh báo hàng sắp hết: tồn kho <= 10 tô nền đỏ nhạt; hàng ngừng kinh doanh chữ xám
        private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductDto p || e.CellStyle == null) return;

            if (!p.IsActive)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
            else if (p.StockQuantity <= LowStockThreshold)
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.SelectionBackColor = Color.IndianRed;
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _page = 1;
            await LoadDataAsync();
        }

        private async void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (_page <= 1) return;
            _page--;
            await LoadDataAsync();
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_page >= _totalPages) return;
            _page++;
            await LoadDataAsync();
        }

        // Xuất toàn bộ mặt hàng (không chỉ trang đang xem) kèm tồn kho ra tệp CSV mở bằng Excel
        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (!UiHelper.RequireAdmin("xuất danh mục hàng (có giá nhập)")) return;
            await UiHelper.RunBusyAsync(this, async () =>
            {
                var all = await ApiClient.GetAsync<List<ProductDto>>($"products?{FilterQuery()}");
                var saved = CsvExporter.SaveWithDialog(this, $"DanhMucHang_{DateTime.Now:yyyyMMdd_HHmm}.csv", all,
                    new("Mã hàng", p => p.ProductCode),
                    new("Mã vạch", p => p.Barcode),
                    new("Tên hàng", p => p.ProductName),
                    new("Nhóm hàng", p => p.CategoryName),
                    new("ĐVT", p => p.Unit),
                    new("Giá nhập", p => p.CostPrice),
                    new("Giá bán", p => p.SalePrice),
                    new("Tồn kho", p => p.StockQuantity),
                    new("Sắp hết hàng", p => p.IsActive && p.StockQuantity <= LowStockThreshold),
                    new("Đang kinh doanh", p => p.IsActive));
                if (saved) UiHelper.ShowInfo($"Đã xuất {all.Count} mặt hàng ra tệp CSV.");
            });
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            cboFilterCategory.SelectedIndex = cboFilterCategory.Items.Count > 0 ? 0 : -1;
            chkShowInactive.Checked = false;
            _page = 1;
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
            txtBarcode.Text = p.Barcode ?? string.Empty;
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
            else if (txtBarcode.Text.Trim().Length > 0 && !IsValidBarcode(txtBarcode.Text.Trim()))
                error = "Mã vạch chỉ gồm 8 – 14 chữ số (để trống nếu hàng chưa có mã vạch)!";
            else if (string.IsNullOrWhiteSpace(txtName.Text)) error = "Vui lòng nhập tên hàng!";
            else if (cboCategory.SelectedValue == null) error = "Vui lòng chọn nhóm hàng!";
            else if (string.IsNullOrWhiteSpace(txtUnit.Text)) error = "Vui lòng nhập đơn vị tính!";
            else if (nudSalePrice.Value <= 0) error = "Giá bán phải lớn hơn 0!";

            if (error != null) UiHelper.ShowWarning(error);
            return error == null;
        }

        private static bool IsValidBarcode(string barcode) =>
            barcode.Length is >= 8 and <= 14 && barcode.All(char.IsAsciiDigit);

        private object BuildRequest() => new
        {
            ProductCode = txtCode.Text.Trim(),
            Barcode = txtBarcode.Text.Trim(),
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
            if (!UiHelper.RequireAdmin("thêm mặt hàng")) return;
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
            if (!UiHelper.RequireAdmin("cập nhật mặt hàng")) return;
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
            if (!UiHelper.RequireAdmin("xóa mặt hàng")) return;
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
            txtBarcode.Clear();
            txtName.Clear();
            txtUnit.Clear();
            txtDescription.Clear();
            nudCostPrice.Value = nudSalePrice.Value = nudStock.Value = 0;
            chkActive.Checked = true;
            txtCode.Focus();
        }
    }
}
