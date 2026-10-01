using ST_FE.Helpers;
using ST_FE.Services;

namespace ST_FE.Forms
{
    public partial class FormMain : Form
    {
        private Form? _currentChild;
        private Button? _currentButton;
        private bool _logoutDone;

        // true = quay lại màn hình đăng nhập sau khi đóng form (đăng xuất / hết phiên)
        public bool ReturnToLogin { get; private set; }

        public FormMain()
        {
            InitializeComponent();

            lblUser.Text = $"👤 {Session.User?.FullName}  ({Session.RoleDisplay})";
            lblServer.Text = "Máy chủ: " + ApiClient.BaseUrl;

            // Phân quyền giao diện theo claim "role" trong JWT.
            // (Chỉ để tiện dụng — việc chặn thật sự nằm ở [Authorize(Roles = "Admin")] phía server)
            btnDashboard.Visible = Session.IsAdmin;
            btnImports.Visible = Session.IsAdmin;
            btnUsers.Visible = Session.IsAdmin;

            ApiClient.SessionExpired += ApiClient_SessionExpired;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Session.IsAdmin)
                btnDashboard.PerformClick();
            else
                btnSales.PerformClick();
        }

        // Nhúng form con vào vùng nội dung bên phải
        private void ShowChild(Form child, Button button)
        {
            _currentChild?.Close();
            _currentChild?.Dispose();

            if (_currentButton != null) _currentButton.BackColor = Color.Transparent;
            button.BackColor = UiHelper.Primary;
            _currentButton = button;

            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(child);
            lblPageTitle.Text = button.Text[(button.Text.IndexOf(' ') + 1)..].Trim();   // bỏ biểu tượng đầu dòng
            child.Show();
            _currentChild = child;
        }

        private void btnDashboard_Click(object sender, EventArgs e) => ShowChild(new FormDashboard(), btnDashboard);
        private void btnSales_Click(object sender, EventArgs e) => ShowChild(new FormSales(), btnSales);
        private void btnOrders_Click(object sender, EventArgs e) => ShowChild(new FormOrders(), btnOrders);
        private void btnProducts_Click(object sender, EventArgs e) => ShowChild(new FormProducts(), btnProducts);
        private void btnCategories_Click(object sender, EventArgs e) => ShowChild(new FormCategoryManagement(), btnCategories);
        private void btnCustomers_Click(object sender, EventArgs e) => ShowChild(new FormCustomers(), btnCustomers);
        private void btnImports_Click(object sender, EventArgs e) => ShowChild(new FormImports(), btnImports);
        private void btnUsers_Click(object sender, EventArgs e) => ShowChild(new FormUsers(), btnUsers);

        private void btnTokenInfo_Click(object sender, EventArgs e)
        {
            using var f = new FormTokenInfo();
            f.ShowDialog(this);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using var f = new FormChangePassword();
            f.ShowDialog(this);
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            if (!UiHelper.Confirm("Bạn có chắc muốn đăng xuất?")) return;

            await ApiClient.LogoutAsync();   // Thu hồi refresh token trên server
            _logoutDone = true;
            ReturnToLogin = true;
            Close();
        }

        // Refresh token hết hạn / bị thu hồi -> buộc đăng nhập lại
        private void ApiClient_SessionExpired(object? sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (IsDisposed || ReturnToLogin) return;
                _logoutDone = true;
                ReturnToLogin = true;
                MessageBox.Show(this, "Phiên đăng nhập đã hết hạn hoặc bị thu hồi.\nVui lòng đăng nhập lại.",
                    "Hết phiên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            });
        }

        // Đếm ngược thời gian còn lại của access token (minh họa cơ chế tự động làm mới)
        private void timerClock_Tick(object sender, EventArgs e)
        {
            if (!Session.IsLoggedIn)
            {
                lblTokenExpiry.Text = "Chưa đăng nhập";
                return;
            }
            var remaining = Session.AccessTokenExpiresAt - DateTime.UtcNow;
            lblTokenExpiry.Text = remaining > TimeSpan.Zero
                ? $"Access token còn hiệu lực: {remaining:mm\\:ss}"
                : "Access token hết hạn — sẽ tự làm mới ở lần gọi API kế tiếp";
        }

        // Đóng cửa sổ bằng nút X cũng thu hồi refresh token
        private async void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_logoutDone || !Session.IsLoggedIn) return;

            e.Cancel = true;
            _logoutDone = true;
            await ApiClient.LogoutAsync();
            Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApiClient.SessionExpired -= ApiClient_SessionExpired;
            timerClock.Stop();
        }
    }
}
