using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;

namespace ST_FE.Forms
{
    // Quản lý tài khoản nhân viên (chỉ Admin mới mở được form này)
    public partial class FormUsers : Form
    {
        private UserDto? _selected;

        public FormUsers()
        {
            InitializeComponent();
            UiHelper.SetupGrid(dgvUsers,
                new GridColumn(nameof(UserDto.UserId), "Mã", 40),
                new GridColumn(nameof(UserDto.Username), "Tên đăng nhập", 110),
                new GridColumn(nameof(UserDto.FullName), "Họ tên", 160),
                new GridColumn(nameof(UserDto.RoleText), "Quyền", 120),
                new GridColumn(nameof(UserDto.IsActive), "Hoạt động", 70, IsCheckBox: true),
                new GridColumn(nameof(UserDto.IsLockedOut), "Bị tạm khóa", 80, IsCheckBox: true),
                new GridColumn(nameof(UserDto.CreatedAt), "Ngày tạo", 110, "dd/MM/yyyy HH:mm"));
            UiHelper.StyleButton(btnAdd, UiHelper.Success);
            UiHelper.StyleButton(btnUpdate, UiHelper.Primary);
            UiHelper.StyleButton(btnResetPassword, UiHelper.Sidebar);
            UiHelper.StyleButton(btnUnlock, UiHelper.Sidebar);

            cboRole.DisplayMember = "Value";
            cboRole.ValueMember = "Key";
            cboRole.DataSource = new List<KeyValuePair<string, string>>
            {
                new("Staff", "Nhân viên bán hàng"),
                new("Admin", "Quản lý")
            };
        }

        private async void FormUsers_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                dgvUsers.DataSource = await ApiClient.GetAsync<List<UserDto>>("users");
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private async void btnReload_Click(object sender, EventArgs e) => await LoadDataAsync();

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            var u = dgvUsers.SelectedItem<UserDto>();
            if (u == null) return;
            _selected = u;
            txtUsername.Text = u.Username;
            txtUsername.ReadOnly = true;   // không cho đổi tên đăng nhập
            txtFullName.Text = u.FullName;
            cboRole.SelectedValue = u.Role;
            chkActive.Checked = u.IsActive;
            txtPassword.Clear();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selected != null)
            {
                UiHelper.ShowWarning("Bấm \"Làm mới\" để nhập tài khoản mới trước khi thêm!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập tên đăng nhập và họ tên!");
                return;
            }
            if (txtPassword.Text.Length < 6)
            {
                UiHelper.ShowWarning("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PostAsync<UserDto>("users", new
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text.Trim(),
                    Role = (string)cboRole.SelectedValue!
                });
                UiHelper.ShowInfo("Tạo tài khoản thành công!");
                ClearInputs();
                await LoadDataAsync();
            });
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn tài khoản cần sửa!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập họ tên!");
                return;
            }

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.PutAsync($"users/{_selected.UserId}", new
                {
                    FullName = txtFullName.Text.Trim(),
                    Role = (string)cboRole.SelectedValue!,
                    IsActive = chkActive.Checked
                });
                UiHelper.ShowInfo("Cập nhật tài khoản thành công!");
                await LoadDataAsync();
            });
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn tài khoản cần đặt lại mật khẩu!");
                return;
            }
            if (txtPassword.Text.Length < 6)
            {
                UiHelper.ShowWarning("Nhập mật khẩu mới (ít nhất 6 ký tự) vào ô Mật khẩu!");
                txtPassword.Focus();
                return;
            }
            if (!UiHelper.Confirm($"Đặt lại mật khẩu cho tài khoản \"{_selected.Username}\"?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                var message = await ApiClient.PostAsync($"users/{_selected.UserId}/reset-password", new { NewPassword = txtPassword.Text });
                UiHelper.ShowInfo(message ?? "Đã đặt lại mật khẩu");
                txtPassword.Clear();
                await LoadDataAsync();
            });
        }

        private async void btnUnlock_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn tài khoản cần mở khóa!");
                return;
            }
            await UiHelper.RunBusyAsync(this, async () =>
            {
                var message = await ApiClient.PostAsync($"users/{_selected.UserId}/unlock");
                UiHelper.ShowInfo(message ?? "Đã mở khóa");
                await LoadDataAsync();
            });
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            dgvUsers.ClearSelection();
            _selected = null;
            txtUsername.ReadOnly = false;
            txtUsername.Clear();
            txtFullName.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 0;
            chkActive.Checked = true;
            txtUsername.Focus();
        }
    }
}
