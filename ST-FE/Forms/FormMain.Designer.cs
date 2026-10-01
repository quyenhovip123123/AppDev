namespace ST_FE.Forms
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            pnlSidebar = new Panel();
            flpMenu = new FlowLayoutPanel();
            btnDashboard = new Button();
            btnSales = new Button();
            btnOrders = new Button();
            btnProducts = new Button();
            btnCategories = new Button();
            btnCustomers = new Button();
            btnImports = new Button();
            btnUsers = new Button();
            pnlAccount = new Panel();
            btnTokenInfo = new Button();
            btnChangePassword = new Button();
            btnLogout = new Button();
            lblLogo = new Label();
            pnlTop = new Panel();
            lblUser = new Label();
            lblPageTitle = new Label();
            pnlContent = new Panel();
            statusStrip = new StatusStrip();
            lblServer = new ToolStripStatusLabel();
            lblTokenExpiry = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            pnlSidebar.SuspendLayout();
            flpMenu.SuspendLayout();
            pnlAccount.SuspendLayout();
            pnlTop.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(31, 41, 55);
            pnlSidebar.Controls.Add(flpMenu);
            pnlSidebar.Controls.Add(pnlAccount);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(230, 698);
            pnlSidebar.TabIndex = 0;
            // 
            // flpMenu
            // 
            flpMenu.Controls.Add(btnDashboard);
            flpMenu.Controls.Add(btnSales);
            flpMenu.Controls.Add(btnOrders);
            flpMenu.Controls.Add(btnProducts);
            flpMenu.Controls.Add(btnCategories);
            flpMenu.Controls.Add(btnCustomers);
            flpMenu.Controls.Add(btnImports);
            flpMenu.Controls.Add(btnUsers);
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.Location = new Point(0, 80);
            flpMenu.Name = "flpMenu";
            flpMenu.Padding = new Padding(0, 10, 0, 0);
            flpMenu.Size = new Size(230, 468);
            flpMenu.TabIndex = 1;
            flpMenu.WrapContents = false;
            // 
            // btnDashboard
            // 
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 10.5F);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 10);
            btnDashboard.Margin = new Padding(0, 0, 0, 2);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(18, 0, 0, 0);
            btnDashboard.Size = new Size(230, 44);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "📊  Tổng quan";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnSales
            // 
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Segoe UI", 10.5F);
            btnSales.ForeColor = Color.White;
            btnSales.Location = new Point(0, 56);
            btnSales.Margin = new Padding(0, 0, 0, 2);
            btnSales.Name = "btnSales";
            btnSales.Padding = new Padding(18, 0, 0, 0);
            btnSales.Size = new Size(230, 44);
            btnSales.TabIndex = 1;
            btnSales.Text = "🛒  Bán hàng";
            btnSales.TextAlign = ContentAlignment.MiddleLeft;
            btnSales.UseVisualStyleBackColor = false;
            btnSales.Click += btnSales_Click;
            // 
            // btnOrders
            // 
            btnOrders.FlatAppearance.BorderSize = 0;
            btnOrders.FlatStyle = FlatStyle.Flat;
            btnOrders.Font = new Font("Segoe UI", 10.5F);
            btnOrders.ForeColor = Color.White;
            btnOrders.Location = new Point(0, 102);
            btnOrders.Margin = new Padding(0, 0, 0, 2);
            btnOrders.Name = "btnOrders";
            btnOrders.Padding = new Padding(18, 0, 0, 0);
            btnOrders.Size = new Size(230, 44);
            btnOrders.TabIndex = 2;
            btnOrders.Text = "🧾  Hóa đơn";
            btnOrders.TextAlign = ContentAlignment.MiddleLeft;
            btnOrders.UseVisualStyleBackColor = false;
            btnOrders.Click += btnOrders_Click;
            // 
            // btnProducts
            // 
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.Font = new Font("Segoe UI", 10.5F);
            btnProducts.ForeColor = Color.White;
            btnProducts.Location = new Point(0, 148);
            btnProducts.Margin = new Padding(0, 0, 0, 2);
            btnProducts.Name = "btnProducts";
            btnProducts.Padding = new Padding(18, 0, 0, 0);
            btnProducts.Size = new Size(230, 44);
            btnProducts.TabIndex = 3;
            btnProducts.Text = "📦  Mặt hàng";
            btnProducts.TextAlign = ContentAlignment.MiddleLeft;
            btnProducts.UseVisualStyleBackColor = false;
            btnProducts.Click += btnProducts_Click;
            // 
            // btnCategories
            // 
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.Font = new Font("Segoe UI", 10.5F);
            btnCategories.ForeColor = Color.White;
            btnCategories.Location = new Point(0, 194);
            btnCategories.Margin = new Padding(0, 0, 0, 2);
            btnCategories.Name = "btnCategories";
            btnCategories.Padding = new Padding(18, 0, 0, 0);
            btnCategories.Size = new Size(230, 44);
            btnCategories.TabIndex = 4;
            btnCategories.Text = "🗂  Nhóm hàng";
            btnCategories.TextAlign = ContentAlignment.MiddleLeft;
            btnCategories.UseVisualStyleBackColor = false;
            btnCategories.Click += btnCategories_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.FlatAppearance.BorderSize = 0;
            btnCustomers.FlatStyle = FlatStyle.Flat;
            btnCustomers.Font = new Font("Segoe UI", 10.5F);
            btnCustomers.ForeColor = Color.White;
            btnCustomers.Location = new Point(0, 240);
            btnCustomers.Margin = new Padding(0, 0, 0, 2);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Padding = new Padding(18, 0, 0, 0);
            btnCustomers.Size = new Size(230, 44);
            btnCustomers.TabIndex = 5;
            btnCustomers.Text = "👥  Khách hàng";
            btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomers.UseVisualStyleBackColor = false;
            btnCustomers.Click += btnCustomers_Click;
            // 
            // btnImports
            // 
            btnImports.FlatAppearance.BorderSize = 0;
            btnImports.FlatStyle = FlatStyle.Flat;
            btnImports.Font = new Font("Segoe UI", 10.5F);
            btnImports.ForeColor = Color.White;
            btnImports.Location = new Point(0, 286);
            btnImports.Margin = new Padding(0, 0, 0, 2);
            btnImports.Name = "btnImports";
            btnImports.Padding = new Padding(18, 0, 0, 0);
            btnImports.Size = new Size(230, 44);
            btnImports.TabIndex = 6;
            btnImports.Text = "📥  Nhập kho";
            btnImports.TextAlign = ContentAlignment.MiddleLeft;
            btnImports.UseVisualStyleBackColor = false;
            btnImports.Click += btnImports_Click;
            // 
            // btnUsers
            // 
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.Font = new Font("Segoe UI", 10.5F);
            btnUsers.ForeColor = Color.White;
            btnUsers.Location = new Point(0, 332);
            btnUsers.Margin = new Padding(0, 0, 0, 2);
            btnUsers.Name = "btnUsers";
            btnUsers.Padding = new Padding(18, 0, 0, 0);
            btnUsers.Size = new Size(230, 44);
            btnUsers.TabIndex = 7;
            btnUsers.Text = "🔐  Nhân viên";
            btnUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Click += btnUsers_Click;
            // 
            // pnlAccount
            // 
            pnlAccount.Controls.Add(btnLogout);
            pnlAccount.Controls.Add(btnChangePassword);
            pnlAccount.Controls.Add(btnTokenInfo);
            pnlAccount.Dock = DockStyle.Bottom;
            pnlAccount.Location = new Point(0, 548);
            pnlAccount.Name = "pnlAccount";
            pnlAccount.Padding = new Padding(0, 10, 0, 10);
            pnlAccount.Size = new Size(230, 150);
            pnlAccount.TabIndex = 2;
            // 
            // btnTokenInfo
            // 
            btnTokenInfo.FlatAppearance.BorderSize = 0;
            btnTokenInfo.FlatStyle = FlatStyle.Flat;
            btnTokenInfo.Font = new Font("Segoe UI", 10.5F);
            btnTokenInfo.ForeColor = Color.White;
            btnTokenInfo.Dock = DockStyle.Top;
            btnTokenInfo.Location = new Point(0, 10);
            btnTokenInfo.Name = "btnTokenInfo";
            btnTokenInfo.Padding = new Padding(18, 0, 0, 0);
            btnTokenInfo.Size = new Size(230, 40);
            btnTokenInfo.TabIndex = 0;
            btnTokenInfo.Text = "🔑  Phiên đăng nhập (JWT)";
            btnTokenInfo.TextAlign = ContentAlignment.MiddleLeft;
            btnTokenInfo.UseVisualStyleBackColor = false;
            btnTokenInfo.Click += btnTokenInfo_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 10.5F);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Dock = DockStyle.Top;
            btnChangePassword.Location = new Point(0, 50);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Padding = new Padding(18, 0, 0, 0);
            btnChangePassword.Size = new Size(230, 40);
            btnChangePassword.TabIndex = 1;
            btnChangePassword.Text = "🔒  Đổi mật khẩu";
            btnChangePassword.TextAlign = ContentAlignment.MiddleLeft;
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnLogout
            // 
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10.5F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Dock = DockStyle.Top;
            btnLogout.Location = new Point(0, 90);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(18, 0, 0, 0);
            btnLogout.Size = new Size(230, 40);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "↩  Đăng xuất";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblLogo
            // 
            lblLogo.BackColor = Color.FromArgb(245, 158, 11);
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(230, 80);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "☀ SUNNY";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblUser);
            pnlTop.Controls.Add(lblPageTitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(230, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1050, 60);
            pnlTop.TabIndex = 1;
            // 
            // lblUser
            // 
            lblUser.Dock = DockStyle.Right;
            lblUser.Font = new Font("Segoe UI", 10F);
            lblUser.ForeColor = Color.FromArgb(75, 85, 99);
            lblUser.Location = new Point(650, 0);
            lblUser.Name = "lblUser";
            lblUser.Padding = new Padding(0, 0, 20, 0);
            lblUser.Size = new Size(400, 60);
            lblUser.TabIndex = 1;
            lblUser.Text = "Người dùng";
            lblUser.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPageTitle
            // 
            lblPageTitle.Dock = DockStyle.Left;
            lblPageTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(31, 41, 55);
            lblPageTitle.Location = new Point(0, 0);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Padding = new Padding(20, 0, 0, 0);
            lblPageTitle.Size = new Size(500, 60);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Tổng quan";
            lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(249, 250, 251);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(230, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1050, 638);
            pnlContent.TabIndex = 2;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblServer, lblTokenExpiry });
            statusStrip.Location = new Point(0, 698);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1280, 22);
            statusStrip.TabIndex = 3;
            // 
            // lblServer
            // 
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(1000, 17);
            lblServer.Spring = true;
            lblServer.Text = "Máy chủ:";
            lblServer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTokenExpiry
            // 
            lblTokenExpiry.Name = "lblTokenExpiry";
            lblTokenExpiry.Size = new Size(200, 17);
            lblTokenExpiry.Text = "Access token:";
            // 
            // timerClock
            // 
            timerClock.Enabled = true;
            timerClock.Interval = 1000;
            timerClock.Tick += timerClock_Tick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContent);
            Controls.Add(pnlTop);
            Controls.Add(pnlSidebar);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 650);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sunny Stationery - Hệ thống quản lý cửa hàng";
            WindowState = FormWindowState.Maximized;
            FormClosing += FormMain_FormClosing;
            FormClosed += FormMain_FormClosed;
            Load += FormMain_Load;
            pnlSidebar.ResumeLayout(false);
            flpMenu.ResumeLayout(false);
            pnlAccount.ResumeLayout(false);
            pnlTop.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlSidebar;
        private FlowLayoutPanel flpMenu;
        private Button btnDashboard;
        private Button btnSales;
        private Button btnOrders;
        private Button btnProducts;
        private Button btnCategories;
        private Button btnCustomers;
        private Button btnImports;
        private Button btnUsers;
        private Panel pnlAccount;
        private Button btnTokenInfo;
        private Button btnChangePassword;
        private Button btnLogout;
        private Label lblLogo;
        private Panel pnlTop;
        private Label lblPageTitle;
        private Label lblUser;
        private Panel pnlContent;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblServer;
        private ToolStripStatusLabel lblTokenExpiry;
        private System.Windows.Forms.Timer timerClock;
    }
}
