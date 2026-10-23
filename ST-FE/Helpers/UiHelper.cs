using ST_FE.Services;

namespace ST_FE.Helpers
{
    // Cột hiển thị trên DataGridView: tên thuộc tính, tiêu đề, độ rộng tương đối, định dạng
    public record GridColumn(string Property, string Header, float Weight = 100, string? Format = null, bool IsCheckBox = false);

    public static class UiHelper
    {
        // Bảng màu "Sunny"
        public static readonly Color Primary = Color.FromArgb(245, 158, 11);
        public static readonly Color PrimaryDark = Color.FromArgb(217, 119, 6);
        public static readonly Color Sidebar = Color.FromArgb(31, 41, 55);
        public static readonly Color SidebarHover = Color.FromArgb(55, 65, 81);
        public static readonly Color Background = Color.FromArgb(249, 250, 251);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);

        public static string Money(decimal value) => value.ToString("N0") + " đ";

        // ===== Thông báo =====

        public static void ShowError(Exception ex)
        {
            // Hết phiên đã được FormMain xử lý (quay về đăng nhập) nên không hiện thêm hộp thoại
            if (ex is ApiException { IsSessionExpired: true }) return;
            // Form con đã bị đóng trong lúc đang chờ API (người dùng chuyển màn hình)
            if (ex is ObjectDisposedException) return;
            // Server trả 403 (VD quyền vừa bị đổi trong lúc đang dùng) -> cùng hộp thoại "Không đủ quyền hạn"
            if (ex is ApiException { StatusCode: System.Net.HttpStatusCode.Forbidden })
            {
                ShowAccessDenied(ex.Message);
                return;
            }

            var message = ex is ApiException ? ex.Message : "Đã xảy ra lỗi: " + ex.Message;
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarning(string message) =>
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void ShowInfo(string message) =>
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Kiểm tra quyền trước khi thao tác: Nhân viên bấm chức năng của Quản lý -> hiện hộp thoại chặn lại.
        // (Chỉ để thông báo thân thiện — việc chặn thật sự vẫn nằm ở [Authorize(Roles = "Admin")] phía server)
        public static bool RequireAdmin(string action) => Require(Session.IsAdmin, action);

        public static bool Require(bool allowed, string action)
        {
            if (allowed) return true;
            ShowAccessDenied($"Tài khoản \"{Session.RoleDisplay}\" không có quyền {action}.");
            return false;
        }

        public static void ShowAccessDenied(string message) =>
            MessageBox.Show(message + "\n\nVui lòng liên hệ Quản lý cửa hàng nếu cần thực hiện chức năng này.",
                "Không đủ quyền hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        public static bool Confirm(string message) =>
            MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        // ===== DataGridView =====

        public static void SetupGrid(DataGridView dgv, params GridColumn[] columns)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Sidebar;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 34;
            dgv.RowTemplate.Height = 30;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 243, 199);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

            dgv.Columns.Clear();
            foreach (var c in columns)
            {
                DataGridViewColumn col = c.IsCheckBox ? new DataGridViewCheckBoxColumn() : new DataGridViewTextBoxColumn();
                col.Name = c.Property;
                col.DataPropertyName = c.Property;
                col.HeaderText = c.Header;
                col.FillWeight = c.Weight;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (c.Format != null)
                {
                    col.DefaultCellStyle.Format = c.Format;
                    if (c.Format.StartsWith("N"))
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                dgv.Columns.Add(col);
            }
        }

        public static T? SelectedItem<T>(this DataGridView dgv) where T : class =>
            dgv.CurrentRow?.DataBoundItem as T;

        // ===== Nút bấm =====

        public static void StyleButton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private static readonly ToolTip LockedTip = new() { InitialDelay = 200, ToolTipTitle = "⊘ Không đủ quyền hạn", ToolTipIcon = ToolTipIcon.Warning };

        // Nút thuộc quyền Quản lý khi đăng nhập bằng Nhân viên: tô xám nhạt (giả lập mờ/disabled), thêm ký hiệu ⊘ (ký tự BMP, emoji như 🔒 không hiển thị được trên Wine),
        // con trỏ ⛔ và tooltip. Vẫn cho bấm để hiện hộp thoại "Không đủ quyền hạn" (UiHelper.RequireAdmin)
        public static void StyleLocked(Button btn)
        {
            var gray = Color.FromArgb(229, 231, 235);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btn.FlatAppearance.MouseOverBackColor = gray;     // không đổi màu khi rê chuột -> không giống nút bấm được
            btn.FlatAppearance.MouseDownBackColor = gray;
            btn.BackColor = gray;
            btn.ForeColor = Color.FromArgb(156, 163, 175);
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            btn.Cursor = Cursors.No;
            if (!btn.Text.StartsWith("⊘"))
            {
                // "✚ Thêm" -> "⊘ Thêm" (⊘ thay biểu tượng cũ), "Hủy hóa đơn" -> "⊘ Hủy hóa đơn"
                var parts = btn.Text.Split(' ', 2);
                var hasIcon = parts.Length == 2 && !parts[0].Any(char.IsLetterOrDigit);
                btn.Text = "⊘ " + (hasIcon ? parts[1] : btn.Text);
            }
            LockedTip.SetToolTip(btn, "Chức năng này chỉ dành cho Quản lý.");
        }

        // Khóa các control trong lúc chờ API để tránh bấm nhiều lần
        public static async Task RunBusyAsync(Control owner, Func<Task> action)
        {
            owner.UseWaitCursor = true;
            owner.Enabled = false;
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                if (!owner.IsDisposed)
                {
                    owner.Enabled = true;
                    owner.UseWaitCursor = false;
                }
            }
        }
    }
}
