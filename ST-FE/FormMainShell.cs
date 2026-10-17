using ST_FE.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ST_FE
{
    public partial class FormMainShell : Form
    {
        private static readonly Color SidebarColor = Color.FromArgb(24, 30, 48);
        private static readonly Color ActiveColor = Color.FromArgb(41, 100, 180);

        // Biến lưu trữ Form con đang được kích hoạt hiển thị
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
        }

        private void FormMainShell_Load(object sender, EventArgs e)
        {
            // 1. Hiển thị thông tin phiên người dùng đăng nhập
            string displayName = string.IsNullOrEmpty(SessionManager.CurrentFullName)
                ? SessionManager.CurrentUsername
                : $"{SessionManager.CurrentFullName} ({SessionManager.CurrentUsername})";
            lblUserInfo.Text = $"Nhân viên: {displayName} | Vai trò: [{SessionManager.CurrentRole}]";

            // 2. Kích hoạt phân quyền giao diện theo vai trò (Role-Based Access)
            if (!ApplyRolePermissions(SessionManager.CurrentRole))
                return;

            // 3. Mở màn hình mặc định tương ứng với vai trò
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        /// <summary>
        /// Hàm nhúng động một Form con vào vùng panelMainContent
        /// </summary>
        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)
        {
            // Đóng form con cũ trước khi mở form mới
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            HighlightActiveButton(senderButton);

            _activeForm = childForm;
            childForm.TopLevel = false;                            // Bỏ thuộc tính cửa sổ độc lập cấp cao nhất
            childForm.FormBorderStyle = FormBorderStyle.None;      // Bỏ viền và thanh điều khiển Windows
            childForm.Dock = DockStyle.Fill;                       // Tràn toàn bộ vùng panelMainContent

            panelMainContent.Controls.Clear();                     // Dọn dẹp màn hình cũ
            panelMainContent.Controls.Add(childForm);              // Thêm form mới vào vùng chứa
            panelMainContent.Tag = childForm;

            lblTitle.Text = screenTitle;                           // Đồng bộ tên màn hình trên Header
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Làm nổi bật nút menu bên Sidebar đang được chọn
        /// </summary>
        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = SidebarColor; // Màu gốc
                }
            }
            activeButton.BackColor = ActiveColor; // Màu xanh active
        }

        /// <summary>
        /// Phân định quyền truy cập hiển thị theo vai trò người dùng
        /// </summary>
        private bool ApplyRolePermissions(string role)
        {
            switch (role.ToUpper())
            {
                case "ADMIN":
                    // Quản trị viên: Có toàn quyền sử dụng tất cả các nút
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    return true;

                case "CASHIER":
                    // Thu ngân: Chỉ truy cập màn hình Bán hàng (POS) và Khách hàng
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    btnCategory.Visible = false;
                    btnProduct.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    return true;

                case "WAREHOUSE":
                    // Thủ kho: Chỉ quản lý Danh mục nhóm hàng và Sản phẩm tồn kho
                    btnPOS.Visible = false;
                    btnCustomer.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    return true;

                default:
                    // Vai trò không xác định: Khóa toàn bộ
                    MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BeginInvoke(new Action(Close));
                    return false;
            }
        }

        /// <summary>
        /// Điều hướng ngay vào màn hình đúng chuyên môn của từng vai trò
        /// </summary>
        private void OpenDefaultScreenByRole(string role)
        {
            switch (role.ToUpper())
            {
                case "ADMIN":
                    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
                    break;
                case "CASHIER":
                    OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
                    break;
                case "WAREHOUSE":
                    OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);
                    break;
            }
        }

        // Lớp bảo vệ thứ 2 (cấp Action): kiểm tra lại vai trò ngay cả khi nút bị lộ ra
        private bool HasRole(params string[] roles)
        {
            foreach (var r in roles)
            {
                if (string.Equals(SessionManager.CurrentRole, r, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return false;
        }

        // ================= CÁC SỰ KIỆN CLICK NÚT TRÊN SIDEBAR =================

        // 1. Quản lý Danh mục (Admin & Warehouse)
        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (!HasRole("Admin", "Warehouse")) return;
            OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
        }

        // 2. Bán hàng POS (Admin & Cashier)
        private void btnPOS_Click(object sender, EventArgs e)
        {
            if (!HasRole("Admin", "Cashier")) return;
            OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
        }

        // 3. Quản lý Sản phẩm (Admin & Warehouse)
        private void btnProduct_Click(object sender, EventArgs e)
        {
            if (!HasRole("Admin", "Warehouse")) return;
            OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);
        }

        // 4. Quản lý Khách hàng (Admin & Cashier)
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (!HasRole("Admin", "Cashier")) return;
            OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
        }

        // 5. Báo cáo Doanh thu (Chỉ Admin)
        private void btnReports_Click(object sender, EventArgs e)
        {
            if (SessionManager.CurrentRole != "Admin")
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT CA BÁN", btnReports);
        }

        // 6. Quản trị Tài khoản Người dùng (Chỉ Admin)
        private void btnUserManage_Click(object sender, EventArgs e)
        {
            if (!HasRole("Admin")) return;
            OpenChildForm(new FormUserManagement(), "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG", btnUserManage);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất phiên làm việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                SessionManager.Clear();

                this.Hide();
                new Login().ShowDialog();
                this.Close();
            }
        }
    }
}
