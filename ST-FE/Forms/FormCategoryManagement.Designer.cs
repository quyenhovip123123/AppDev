namespace ST_FE.Forms
{
    partial class FormCategoryManagement
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
            dgvCategories = new DataGridView();
            grpInfo = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblCategoryName = new Label();
            txtCategoryName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            pnlSearch = new Panel();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnReload = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            grpInfo.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCategories
            // 
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.Location = new Point(12, 64);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.Size = new Size(580, 500);
            dgvCategories.TabIndex = 0;
            dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(lblId);
            grpInfo.Controls.Add(txtId);
            grpInfo.Controls.Add(lblCategoryName);
            grpInfo.Controls.Add(txtCategoryName);
            grpInfo.Controls.Add(lblDescription);
            grpInfo.Controls.Add(txtDescription);
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
            grpInfo.Text = "Thông tin nhóm hàng";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 30);
            lblId.Name = "lblId";
            lblId.TabIndex = 0;
            lblId.Text = "Mã nhóm";
            // 
            // txtId
            // 
            txtId.Location = new Point(20, 52);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(310, 25);
            txtId.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(20, 89);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên nhóm hàng *";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(20, 111);
            txtCategoryName.MaxLength = 100;
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(310, 25);
            txtCategoryName.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 148);
            lblDescription.Name = "lblDescription";
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô tả";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 170);
            txtDescription.MaxLength = 255;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(310, 80);
            txtDescription.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 268);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 38);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(180, 268);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(150, 38);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "✎ Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 314);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 38);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "✖ Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(180, 314);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 38);
            btnClear.TabIndex = 9;
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
            txtKeyword.PlaceholderText = "Tên nhóm hàng...";
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
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvCategories);
            Controls.Add(grpInfo);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 10F);
            Name = "FormCategoryManagement";
            Padding = new Padding(12);
            Text = "Nhóm hàng";
            Load += FormCategoryManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCategories;
        private GroupBox grpInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblDescription;
        private TextBox txtDescription;
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
