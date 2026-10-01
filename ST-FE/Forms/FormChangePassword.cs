using ST_FE.Helpers;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormChangePassword : Form
    {
        public FormChangePassword()
        {
            InitializeComponent();
            UiHelper.StyleButton(btnSave, UiHelper.Primary);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrent.Text) || string.IsNullOrEmpty(txtNew.Text))
            {
                UiHelper.ShowWarning("Vui lòng nhập đầy đủ mật khẩu!");
                return;
            }
            if (txtNew.Text.Length < 6)
            {
                UiHelper.ShowWarning("Mật khẩu mới phải có ít nhất 6 ký tự!");
                return;
            }
            if (txtNew.Text != txtConfirm.Text)
            {
                UiHelper.ShowWarning("Mật khẩu nhập lại không khớp!");
                return;
            }

            await UiHelper.RunBusyAsync(this, async () =>
            {
                await ApiClient.ChangePasswordAsync(txtCurrent.Text, txtNew.Text);
                UiHelper.ShowInfo("Đổi mật khẩu thành công!");
                DialogResult = DialogResult.OK;
                Close();
            });
        }
    }
}
