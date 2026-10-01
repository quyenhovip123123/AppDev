namespace ST_FE.Forms
{
    partial class FormCustomers
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
            dgvCustomers = new DataGridView();
            grpInfo = new GroupBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPoints = new Label();
            txtPoints = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            pnlSearch = new Panel();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnReload = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpInfo.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(12, 64);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(580, 500);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(lblFullName);
            grpInfo.Controls.Add(txtFullName);
            grpInfo.Controls.Add(lblPhone);
            grpInfo.Controls.Add(txtPhone);
            grpInfo.Controls.Add(lblEmail);
            grpInfo.Controls.Add(txtEmail);
            grpInfo.Controls.Add(lblAddress);
            grpInfo.Controls.Add(txtAddress);
            grpInfo.Controls.Add(lblPoints);
            grpInfo.Controls.Add(txtPoints);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnClear);
            grpInfo.Dock = DockStyle.Right;
            grpInfo.Location = new Point(600, 64);
            grpInfo.Name = "grpInfo";
            grpInfo.Padding = new Padding(10);
            grpInfo.Size = new Size(350, 500);
            grpInfo.TabIndex = 1;
            grpInfo.Text = "Thông tin khách hàng";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 30);
            lblFullName.Name = "lblFullName";
            lblFullName.TabIndex = 0;
            lblFullName.Text = "Họ tên *";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(20, 52);
            txtFullName.MaxLength = 100;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(310, 25);
            txtFullName.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(20, 89);
            lblPhone.Name = "lblPhone";
            lblPhone.TabIndex = 2;
            lblPhone.Text = "Số điện thoại *";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(20, 111);
            txtPhone.MaxLength = 11;
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(310, 25);
            txtPhone.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 148);
            lblEmail.Name = "lblEmail";
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(20, 170);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(310, 25);
            txtEmail.TabIndex = 5;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 207);
            lblAddress.Name = "lblAddress";
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Địa chỉ";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(20, 229);
            txtAddress.MaxLength = 255;
            txtAddress.Multiline = true;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(310, 60);
            txtAddress.TabIndex = 7;
            // 
            // lblPoints
            // 
            lblPoints.AutoSize = true;
            lblPoints.Location = new Point(20, 301);
            lblPoints.Name = "lblPoints";
            lblPoints.TabIndex = 8;
            lblPoints.Text = "Điểm tích lũy (10.000đ = 1 điểm)";
            // 
            // txtPoints
            // 
            txtPoints.Location = new Point(20, 323);
            txtPoints.Name = "txtPoints";
            txtPoints.ReadOnly = true;
            txtPoints.Size = new Size(310, 25);
            txtPoints.TabIndex = 9;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 366);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 38);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(180, 366);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(150, 38);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "✎ Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 412);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 38);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "✖ Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(180, 412);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 38);
            btnClear.TabIndex = 13;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtKeyword);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(btnReload);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(12, 12);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(0, 0, 0, 10);
            pnlSearch.Size = new Size(900, 52);
            pnlSearch.TabIndex = 2;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(0, 8);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Tên hoặc số điện thoại...";
            txtKeyword.Size = new Size(300, 25);
            txtKeyword.TabIndex = 0;
            txtKeyword.KeyDown += txtKeyword_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(310, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 32);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "🔍 Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(430, 5);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(100, 32);
            btnReload.TabIndex = 2;
            btnReload.Text = "⟳ Tải lại";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // FormCustomers
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvCustomers);
            Controls.Add(grpInfo);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 10F);
            Name = "FormCustomers";
            Padding = new Padding(12);
            Text = "Khách hàng";
            Load += FormCustomers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCustomers;
        private GroupBox grpInfo;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPoints;
        private TextBox txtPoints;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Panel pnlSearch;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnReload;
    }
}
