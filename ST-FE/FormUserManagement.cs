using ST_FE.WinForms;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace ST_FE
{
    public partial class FormUserManagement : Form
    {
        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new string[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1;
        }

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");
                dgvUsers.DataSource = users;
                ConfigureGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            if (dgvUsers.Columns.Count == 0)
                return;

            dgvUsers.Columns["Id"].HeaderText = "ID";
            dgvUsers.Columns["Id"].FillWeight = 30;
            dgvUsers.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgvUsers.Columns["FullName"].HeaderText = "Họ tên";
            dgvUsers.Columns["Email"].HeaderText = "Email";
            dgvUsers.Columns["Role"].HeaderText = "Vai trò";
            dgvUsers.Columns["IsActive"].HeaderText = "Hoạt động";
            dgvUsers.Columns["IsActive"].FillWeight = 50;
        }

        private UserDto? GetSelectedUser()
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is UserDto user)
                return user;

            MessageBox.Show("Vui lòng chọn một tài khoản trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier"
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtFullName.Clear();
                    txtEmail.Clear();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnResetPassword_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null)
                return;

            if (MessageBox.Show($"Đặt lại mật khẩu của '{user.Username}' về 123456?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var res = await ApiClientService.Client.PutAsync($"users/{user.Id}/reset-password", null);
                if (res.IsSuccessStatusCode)
                    MessageBox.Show("Đã đặt lại mật khẩu về 123456!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Đặt lại mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnToggleLock_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null)
                return;

            // Không cho tự khóa chính tài khoản đang đăng nhập
            if (string.Equals(user.Username, SessionManager.CurrentUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể khóa tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string action = user.IsActive ? "KHÓA" : "MỞ KHÓA";
            if (MessageBox.Show($"Xác nhận {action} tài khoản '{user.Username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var res = await ApiClientService.Client.PutAsync($"users/{user.Id}/toggle-lock", null);
                if (res.IsSuccessStatusCode)
                    await LoadUsersAsync();
                else
                    MessageBox.Show("Thao tác thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
