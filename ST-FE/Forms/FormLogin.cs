using ST_FE.Helpers;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            UiHelper.StyleButton(btnLogin, UiHelper.Primary);
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập tên đăng nhập và mật khẩu!");
                return;
            }

            btnLogin.Text = "Đang đăng nhập...";
            await UiHelper.RunBusyAsync(this, async () =>
            {
                // Gửi POST /api/auth/login, nhận access token + refresh token và lưu vào Session
                await ApiClient.LoginAsync(txtUsername.Text.Trim(), txtPassword.Text);
                DialogResult = DialogResult.OK;
                Close();
            });
            btnLogin.Text = "ĐĂNG NHẬP";
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
