using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormCategoryManagement : Form
    {
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

            // Nhân viên chỉ được xem (server cũng chặn bằng [Authorize(Roles = "Admin")])
            if (!Session.IsAdmin)
            {
                btnAdd.Enabled = btnUpdate.Enabled = btnDelete.Enabled = false;
                txtCategoryName.ReadOnly = txtDescription.ReadOnly = true;
                grpInfo.Text += " (chỉ xem)";
            }
        }

        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // GET /api/categories hoặc /api/categories/search?keyword=...
        private async Task LoadDataAsync(string? keyword = null)
        {
            try
            {
                var url = string.IsNullOrEmpty(keyword) ? "categories" : $"categories/search?keyword={Uri.EscapeDataString(keyword)}";
                dgvCategories.DataSource = await ApiClient.GetAsync<List<CategoryDto>>(url);
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadDataAsync(txtKeyword.Text.Trim());
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
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
