namespace ST_FE
{
    partial class FormUserManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            panelDetail = new Panel();
            lblDetailTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panelDetail.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeight = 36;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.TabIndex = 0;
            // 
            // panelDetail
            // 
            panelDetail.BackColor = Color.White;
            panelDetail.Controls.Add(lblDetailTitle);
            panelDetail.Controls.Add(lblUsername);
            panelDetail.Controls.Add(txtUsername);
            panelDetail.Controls.Add(lblPassword);
            panelDetail.Controls.Add(txtPassword);
            panelDetail.Controls.Add(lblFullName);
            panelDetail.Controls.Add(txtFullName);
            panelDetail.Controls.Add(lblEmail);
            panelDetail.Controls.Add(txtEmail);
            panelDetail.Controls.Add(lblRole);
            panelDetail.Controls.Add(cboRole);
            panelDetail.Controls.Add(btnAddUser);
            panelDetail.Controls.Add(btnResetPassword);
            panelDetail.Controls.Add(btnToggleLock);
            panelDetail.Dock = DockStyle.Right;
            panelDetail.Name = "panelDetail";
            panelDetail.Size = new Size(320, 621);
            panelDetail.TabIndex = 1;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblDetailTitle.Location = new Point(20, 15);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Text = "Thông tin tài khoản";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(20, 60);
            lblUsername.Name = "lblUsername";
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(20, 82);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 25);
            txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(20, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(20, 142);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 25);
            txtPassword.TabIndex = 1;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F);
            lblFullName.Location = new Point(20, 180);
            lblFullName.Name = "lblFullName";
            lblFullName.Text = "Họ và tên:";
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(20, 202);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(280, 25);
            txtFullName.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(20, 240);
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(20, 262);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(280, 25);
            txtEmail.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 10F);
            lblRole.Location = new Point(20, 300);
            lblRole.Name = "lblRole";
            lblRole.Text = "Vai trò:";
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("Segoe UI", 10F);
            cboRole.Location = new Point(20, 322);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(280, 25);
            cboRole.TabIndex = 4;
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.FromArgb(40, 167, 69);
            btnAddUser.Cursor = Cursors.Hand;
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(20, 370);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(280, 40);
            btnAddUser.TabIndex = 10;
            btnAddUser.Text = "＋ Tạo tài khoản";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnResetPassword
            // 
            btnResetPassword.BackColor = Color.FromArgb(255, 140, 0);
            btnResetPassword.Cursor = Cursors.Hand;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.Location = new Point(20, 420);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(280, 40);
            btnResetPassword.TabIndex = 11;
            btnResetPassword.Text = "🔑 Reset mật khẩu";
            btnResetPassword.UseVisualStyleBackColor = false;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // btnToggleLock
            // 
            btnToggleLock.BackColor = Color.FromArgb(220, 53, 69);
            btnToggleLock.Cursor = Cursors.Hand;
            btnToggleLock.FlatAppearance.BorderSize = 0;
            btnToggleLock.FlatStyle = FlatStyle.Flat;
            btnToggleLock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnToggleLock.ForeColor = Color.White;
            btnToggleLock.Location = new Point(20, 470);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(280, 40);
            btnToggleLock.TabIndex = 12;
            btnToggleLock.Text = "🔒 Khóa / Mở khóa";
            btnToggleLock.UseVisualStyleBackColor = false;
            btnToggleLock.Click += btnToggleLock_Click;
            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 621);
            Controls.Add(dgvUsers);
            Controls.Add(panelDetail);
            Name = "FormUserManagement";
            Text = "Quản trị Tài khoản";
            Load += FormUserManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panelDetail.ResumeLayout(false);
            panelDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsers;
        private Panel panelDetail;
        private Label lblDetailTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnToggleLock;
    }
}
