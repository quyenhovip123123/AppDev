namespace ST_FE.Forms
{
    partial class FormUsers
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            grpInfo = new GroupBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkActive = new CheckBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnResetPassword = new Button();
            btnUnlock = new Button();
            btnClear = new Button();
            lblSecurityNote = new Label();
            pnlSearch = new Panel();
            btnReload = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpInfo.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(12, 64);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(580, 500);
            dgvUsers.TabIndex = 0;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(lblUsername);
            grpInfo.Controls.Add(txtUsername);
            grpInfo.Controls.Add(lblFullName);
            grpInfo.Controls.Add(txtFullName);
            grpInfo.Controls.Add(lblRole);
            grpInfo.Controls.Add(cboRole);
            grpInfo.Controls.Add(lblPassword);
            grpInfo.Controls.Add(txtPassword);
            grpInfo.Controls.Add(chkActive);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnResetPassword);
            grpInfo.Controls.Add(btnUnlock);
            grpInfo.Controls.Add(btnClear);
            grpInfo.Controls.Add(lblSecurityNote);
            grpInfo.Dock = DockStyle.Right;
            grpInfo.Location = new Point(600, 64);
            grpInfo.Name = "grpInfo";
            grpInfo.Padding = new Padding(10);
            grpInfo.Size = new Size(350, 500);
            grpInfo.TabIndex = 1;
            grpInfo.Text = "Thông tin tài khoản";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Tên đăng nhập *";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(20, 52);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(310, 25);
            txtUsername.TabIndex = 1;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 89);
            lblFullName.Name = "lblFullName";
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Họ tên *";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(20, 111);
            txtFullName.MaxLength = 100;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(310, 25);
            txtFullName.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 148);
            lblRole.Name = "lblRole";
            lblRole.TabIndex = 4;
            lblRole.Text = "Quyền *";
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Location = new Point(20, 170);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(310, 25);
            cboRole.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 207);
            lblPassword.Name = "lblPassword";
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Mật khẩu (khi thêm mới / đặt lại)";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 229);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(310, 25);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkActive
            // 
            chkActive.CheckState = CheckState.Checked;
            chkActive.Checked = true;
            chkActive.Location = new Point(20, 266);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(310, 25);
            chkActive.TabIndex = 8;
            chkActive.Text = "Đang hoạt động";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 309);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 38);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(180, 309);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(150, 38);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "✎ Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnResetPassword
            // 
            btnResetPassword.Location = new Point(20, 355);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(150, 38);
            btnResetPassword.TabIndex = 11;
            btnResetPassword.Text = "🔑 Đặt lại MK";
            btnResetPassword.UseVisualStyleBackColor = true;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // btnUnlock
            // 
            btnUnlock.Location = new Point(180, 355);
            btnUnlock.Name = "btnUnlock";
            btnUnlock.Size = new Size(150, 38);
            btnUnlock.TabIndex = 12;
            btnUnlock.Text = "🔓 Mở khóa";
            btnUnlock.UseVisualStyleBackColor = true;
            btnUnlock.Click += btnUnlock_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(20, 401);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 38);
            btnClear.TabIndex = 13;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblSecurityNote
            // 
            lblSecurityNote.ForeColor = Color.FromArgb(107, 114, 128);
            lblSecurityNote.Location = new Point(20, 460);
            lblSecurityNote.Name = "lblSecurityNote";
            lblSecurityNote.Size = new Size(310, 90);
            lblSecurityNote.TabIndex = 14;
            lblSecurityNote.Text = "Khi khóa tài khoản, đổi quyền hoặc đặt lại mật khẩu, mọi token đang dùng của tài khoản đó sẽ bị thu hồi ngay lập tức.";
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnReload);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(12, 12);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(0, 0, 0, 10);
            pnlSearch.Size = new Size(900, 52);
            pnlSearch.TabIndex = 2;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(0, 5);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(100, 32);
            btnReload.TabIndex = 0;
            btnReload.Text = "⟳ Tải lại";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // FormUsers
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvUsers);
            Controls.Add(grpInfo);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 10F);
            Name = "FormUsers";
            Padding = new Padding(12);
            Text = "Nhân viên";
            Load += FormUsers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsers;
        private GroupBox grpInfo;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cboRole;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkActive;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnResetPassword;
        private Button btnUnlock;
        private Button btnClear;
        private Label lblSecurityNote;
        private Panel pnlSearch;
        private Button btnReload;
    }
}
