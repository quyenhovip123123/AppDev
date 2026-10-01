using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormCustomers : Form
    {
        private int? _selectedId;

        public FormCustomers()
        {
            InitializeComponent();
            UiHelper.SetupGrid(dgvCustomers,
                new GridColumn(nameof(CustomerDto.CustomerId), "Mã", 40),
                new GridColumn(nameof(CustomerDto.FullName), "Họ tên", 150),
                new GridColumn(nameof(CustomerDto.Phone), "Điện thoại", 90),
                new GridColumn(nameof(CustomerDto.Email), "Email", 130),
                new GridColumn(nameof(CustomerDto.Address), "Địa chỉ", 130),
                new GridColumn(nameof(CustomerDto.Points), "Điểm", 50, "N0"));
            UiHelper.StyleButton(btnAdd, UiHelper.Success);
            UiHelper.StyleButton(btnUpdate, UiHelper.Primary);
            UiHelper.StyleButton(btnDelete, UiHelper.Danger);

            // Nhân viên được thêm/sửa khách hàng nhưng không được xóa
            btnDelete.Enabled = Session.IsAdmin;
        }

        private async void FormCustomers_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var url = $"customers?keyword={Uri.EscapeDataString(txtKeyword.Text.Trim())}";
                dgvCustomers.DataSource = await ApiClient.GetAsync<List<CustomerDto>>(url);
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await LoadDataAsync();

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

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            var c = dgvCustomers.SelectedItem<CustomerDto>();
            if (c == null) return;
            _selectedId = c.CustomerId;
            txtFullName.Text = c.FullName;
            txtPhone.Text = c.Phone;
            txtEmail.Text = c.Email ?? string.Empty;
            txtAddress.Text = c.Address ?? string.Empty;
            txtPoints.Text = c.Points.ToString("N0");
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập họ tên khách hàng!");
                return false;
            }
            var phone = txtPhone.Text.Trim();
            if (phone.Length < 10 || !phone.StartsWith('0') || !phone.All(char.IsDigit))
            {
                UiHelper.ShowWarning("Số điện thoại phải bắt đầu bằng 0 và có 10-11 chữ số!");
                return false;
            }
            return true;
        }

        private object BuildRequest() => new
        {
            FullName = txtFullName.Text.Trim(),
            Phone = txtPhone.Text.Trim(),
            Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
            Address = txtAddress.Text.Trim()
        };

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PostAsync<CustomerDto>("customers", BuildRequest());
                UiHelper.ShowInfo("Thêm khách hàng thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn khách hàng cần sửa!");
                return;
            }
            if (!ValidateInput()) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PutAsync($"customers/{_selectedId}", BuildRequest());
                UiHelper.ShowInfo("Cập nhật thành công!");
                await LoadDataAsync();
            });
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn khách hàng cần xóa!");
                return;
            }
            if (!UiHelper.Confirm($"Bạn có chắc muốn xóa khách hàng \"{txtFullName.Text}\"?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.DeleteAsync($"customers/{_selectedId}");
                UiHelper.ShowInfo("Xóa thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            dgvCustomers.ClearSelection();
            _selectedId = null;
            txtFullName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtPoints.Clear();
            txtFullName.Focus();
        }
    }
}
