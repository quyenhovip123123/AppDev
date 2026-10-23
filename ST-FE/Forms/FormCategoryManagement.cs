using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormCategoryManagement : Form
    {
        private readonly PagerBar _pager = new("nhóm hàng");

        public FormCategoryManagement()
        {
            InitializeComponent();
            UiHelper.SetupGrid(dgvCategories,
                new GridColumn(nameof(CategoryDto.CategoryId), "Mã", 50),
                new GridColumn(nameof(CategoryDto.CategoryName), "Tên nhóm hàng", 180),
                new GridColumn(nameof(CategoryDto.Description), "Mô tả", 250),
                new GridColumn(nameof(CategoryDto.ProductCount), "Số mặt hàng", 80, "N0"));
            UiHelper.StyleButton(btnAdd, UiHelper.Success);
            UiHelper.StyleButton(btnUpdate, UiHelper.Primary);
            UiHelper.StyleButton(btnDelete, UiHelper.Danger);

            // Nhân viên chỉ được xem: bấm Thêm/Sửa/Xóa sẽ hiện hộp thoại "Không đủ quyền hạn"
            // (server cũng chặn bằng [Authorize(Roles = "Admin")])
            if (!Session.IsAdmin)
            {
                foreach (var btn in new[] { btnAdd, btnUpdate, btnDelete }) UiHelper.StyleLocked(btn);
                txtCategoryName.ReadOnly = txtDescription.ReadOnly = true;
                grpInfo.Text += " (chỉ xem)";
            }

            _pager.AttachBelow(dgvCategories).PageChanged += async (_, _) => await LoadDataAsync();
        }

        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // GET /api/categories/paged?keyword=...&page=...&pageSize=... (phân trang phía máy chủ)
        private async Task LoadDataAsync()
        {
            try
            {
                var url = $"categories/paged?keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}&{_pager.Query}";
                var result = await ApiClient.GetAsync<PagedResult<CategoryDto>>(url);
                if (!_pager.Apply(result))
                {
                    await LoadDataAsync();
                    return;
                }
                dgvCategories.DataSource = result.Items;
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _pager.Reset();
            await LoadDataAsync();
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
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

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            var cat = dgvCategories.SelectedItem<CategoryDto>();
            if (cat == null) return;
            txtId.Text = cat.CategoryId.ToString();
            txtCategoryName.Text = cat.CategoryName;
            txtDescription.Text = cat.Description ?? string.Empty;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                UiHelper.ShowWarning("Tên nhóm hàng không được để trống!");
                txtCategoryName.Focus();
                return false;
            }
            return true;
        }

        private object BuildRequest() => new
        {
            CategoryName = txtCategoryName.Text.Trim(),
            Description = txtDescription.Text.Trim()
        };

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!UiHelper.RequireAdmin("thêm nhóm hàng")) return;
            if (!ValidateInput()) return;
            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PostAsync<CategoryDto>("categories", BuildRequest());
                UiHelper.ShowInfo("Thêm nhóm hàng thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!UiHelper.RequireAdmin("cập nhật nhóm hàng")) return;
            if (string.IsNullOrEmpty(txtId.Text))
            {
                UiHelper.ShowWarning("Vui lòng chọn nhóm hàng cần sửa!");
                return;
            }
            if (!ValidateInput()) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PutAsync($"categories/{txtId.Text}", BuildRequest());
                UiHelper.ShowInfo("Cập nhật thành công!");
                await LoadDataAsync();
            });
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!UiHelper.RequireAdmin("xóa nhóm hàng")) return;
            if (string.IsNullOrEmpty(txtId.Text))
            {
                UiHelper.ShowWarning("Vui lòng chọn nhóm hàng cần xóa!");
                return;
            }
            if (!UiHelper.Confirm($"Bạn có chắc muốn xóa nhóm hàng \"{txtCategoryName.Text}\"?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.DeleteAsync($"categories/{txtId.Text}");
                UiHelper.ShowInfo("Xóa thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            dgvCategories.ClearSelection();
            txtId.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();
            txtCategoryName.Focus();
        }
    }
}
