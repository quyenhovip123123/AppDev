namespace ST_FE.Forms
{
    partial class FormProducts
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
            dgvProducts = new DataGridView();
            grpInfo = new GroupBox();
            lblCode = new Label();
            txtCode = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblUnit = new Label();
            txtUnit = new TextBox();
            lblCostPrice = new Label();
            nudCostPrice = new NumericUpDown();
            lblSalePrice = new Label();
            nudSalePrice = new NumericUpDown();
            lblStock = new Label();
            nudStock = new NumericUpDown();
            lblDescription = new Label();
            txtDescription = new TextBox();
            chkActive = new CheckBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            pnlSearch = new Panel();
            txtKeyword = new TextBox();
            cboFilterCategory = new ComboBox();
            chkShowInactive = new CheckBox();
            btnSearch = new Button();
            btnReload = new Button();
            btnExport = new Button();
            pnlPager = new Panel();
            btnPrevPage = new Button();
            lblPage = new Label();
            btnNextPage = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCostPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            pnlSearch.SuspendLayout();
            pnlPager.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(12, 64);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(580, 500);
            dgvProducts.TabIndex = 0;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(lblCode);
            grpInfo.Controls.Add(txtCode);
            grpInfo.Controls.Add(lblBarcode);
            grpInfo.Controls.Add(txtBarcode);
            grpInfo.Controls.Add(lblName);
            grpInfo.Controls.Add(txtName);
            grpInfo.Controls.Add(lblCategory);
            grpInfo.Controls.Add(cboCategory);
            grpInfo.Controls.Add(lblUnit);
            grpInfo.Controls.Add(txtUnit);
            grpInfo.Controls.Add(lblCostPrice);
            grpInfo.Controls.Add(nudCostPrice);
            grpInfo.Controls.Add(lblSalePrice);
            grpInfo.Controls.Add(nudSalePrice);
            grpInfo.Controls.Add(lblStock);
            grpInfo.Controls.Add(nudStock);
            grpInfo.Controls.Add(lblDescription);
            grpInfo.Controls.Add(txtDescription);
            grpInfo.Controls.Add(chkActive);
            grpInfo.Controls.Add(btnAdd);
            grpInfo.Controls.Add(btnUpdate);
            grpInfo.Controls.Add(btnDelete);
            grpInfo.Controls.Add(btnClear);
            grpInfo.Dock = DockStyle.Right;
            grpInfo.Location = new Point(600, 64);
            grpInfo.Name = "grpInfo";
            grpInfo.Padding = new Padding(10);
            grpInfo.Size = new Size(380, 500);
            grpInfo.TabIndex = 1;
            grpInfo.Text = "Thông tin mặt hàng";
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(20, 33);
            lblCode.Name = "lblCode";
            lblCode.TabIndex = 0;
            lblCode.Text = "Mã hàng *";
            // 
            // txtCode
            // 
            txtCode.CharacterCasing = CharacterCasing.Upper;
            txtCode.Location = new Point(130, 30);
            txtCode.MaxLength = 30;
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(230, 25);
            txtCode.TabIndex = 1;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(20, 70);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.TabIndex = 2;
            lblBarcode.Text = "Mã vạch";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(130, 67);
            txtBarcode.MaxLength = 14;
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "EAN-13, để trống nếu chưa có";
            txtBarcode.Size = new Size(230, 25);
            txtBarcode.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 107);
            lblName.Name = "lblName";
            lblName.TabIndex = 4;
            lblName.Text = "Tên hàng *";
            // 
            // txtName
            // 
            txtName.Location = new Point(130, 104);
            txtName.MaxLength = 150;
            txtName.Name = "txtName";
            txtName.Size = new Size(230, 25);
            txtName.TabIndex = 5;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(20, 144);
            lblCategory.Name = "lblCategory";
            lblCategory.TabIndex = 6;
            lblCategory.Text = "Nhóm hàng *";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(130, 141);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(230, 25);
            cboCategory.TabIndex = 7;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(20, 181);
            lblUnit.Name = "lblUnit";
            lblUnit.TabIndex = 8;
            lblUnit.Text = "Đơn vị tính *";
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(130, 178);
            txtUnit.MaxLength = 20;
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(230, 25);
            txtUnit.TabIndex = 9;
            // 
            // lblCostPrice
            // 
            lblCostPrice.AutoSize = true;
            lblCostPrice.Location = new Point(20, 218);
            lblCostPrice.Name = "lblCostPrice";
            lblCostPrice.TabIndex = 10;
            lblCostPrice.Text = "Giá nhập (đ)";
            // 
            // nudCostPrice
            // 
            nudCostPrice.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudCostPrice.Location = new Point(130, 215);
            nudCostPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudCostPrice.Name = "nudCostPrice";
            nudCostPrice.Size = new Size(230, 25);
            nudCostPrice.TabIndex = 11;
            nudCostPrice.ThousandsSeparator = true;
            // 
            // lblSalePrice
            // 
            lblSalePrice.AutoSize = true;
            lblSalePrice.Location = new Point(20, 255);
            lblSalePrice.Name = "lblSalePrice";
            lblSalePrice.TabIndex = 12;
            lblSalePrice.Text = "Giá bán (đ) *";
            // 
            // nudSalePrice
            // 
            nudSalePrice.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudSalePrice.Location = new Point(130, 252);
            nudSalePrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudSalePrice.Name = "nudSalePrice";
            nudSalePrice.Size = new Size(230, 25);
            nudSalePrice.TabIndex = 13;
            nudSalePrice.ThousandsSeparator = true;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(20, 292);
            lblStock.Name = "lblStock";
            lblStock.TabIndex = 14;
            lblStock.Text = "Tồn kho";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(130, 289);
            nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(230, 25);
            nudStock.TabIndex = 15;
            nudStock.ThousandsSeparator = true;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 329);
            lblDescription.Name = "lblDescription";
            lblDescription.TabIndex = 16;
            lblDescription.Text = "Mô tả";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(130, 326);
            txtDescription.MaxLength = 255;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(230, 44);
            txtDescription.TabIndex = 17;
            // 
            // chkActive
            // 
            chkActive.CheckState = CheckState.Checked;
            chkActive.Checked = true;
            chkActive.Location = new Point(130, 378);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(230, 25);
            chkActive.TabIndex = 18;
            chkActive.Text = "Đang kinh doanh";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(20, 414);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(165, 38);
            btnAdd.TabIndex = 19;
            btnAdd.Text = "✚ Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(195, 414);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(165, 38);
            btnUpdate.TabIndex = 20;
            btnUpdate.Text = "✎ Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(20, 458);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(165, 38);
            btnDelete.TabIndex = 21;
            btnDelete.Text = "✖ Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(195, 458);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(165, 38);
            btnClear.TabIndex = 22;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtKeyword);
            pnlSearch.Controls.Add(cboFilterCategory);
            pnlSearch.Controls.Add(chkShowInactive);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(btnReload);
            pnlSearch.Controls.Add(btnExport);
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
            txtKeyword.PlaceholderText = "Mã hoặc tên hàng...";
            txtKeyword.Size = new Size(240, 25);
            txtKeyword.TabIndex = 0;
            txtKeyword.KeyDown += txtKeyword_KeyDown;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Location = new Point(250, 8);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(200, 25);
            cboFilterCategory.TabIndex = 1;
            // 
            // chkShowInactive
            // 
            chkShowInactive.Location = new Point(460, 10);
            chkShowInactive.Name = "chkShowInactive";
            chkShowInactive.Size = new Size(160, 25);
            chkShowInactive.TabIndex = 2;
            chkShowInactive.Text = "Hiện hàng ngừng KD";
            chkShowInactive.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(630, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 32);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "⚲ Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnReload
            // 
            btnReload.Location = new Point(750, 5);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(100, 32);
            btnReload.TabIndex = 4;
            btnReload.Text = "⟳ Tải lại";
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(860, 5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(130, 32);
            btnExport.TabIndex = 5;
            btnExport.Text = "▤ Xuất Excel";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // pnlPager
            // 
            pnlPager.Controls.Add(btnPrevPage);
            pnlPager.Controls.Add(lblPage);
            pnlPager.Controls.Add(btnNextPage);
            pnlPager.Dock = DockStyle.Bottom;
            pnlPager.Location = new Point(12, 588);
            pnlPager.Name = "pnlPager";
            pnlPager.Size = new Size(646, 40);
            pnlPager.TabIndex = 3;
            // 
            // btnPrevPage
            // 
            btnPrevPage.Location = new Point(0, 6);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(90, 30);
            btnPrevPage.TabIndex = 0;
            btnPrevPage.Text = "◀ Trước";
            btnPrevPage.UseVisualStyleBackColor = true;
            btnPrevPage.Click += btnPrevPage_Click;
            // 
            // lblPage
            // 
            lblPage.Location = new Point(96, 6);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(300, 30);
            lblPage.TabIndex = 1;
            lblPage.Text = "Trang 1/1";
            lblPage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            btnNextPage.Location = new Point(402, 6);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(90, 30);
            btnNextPage.TabIndex = 2;
            btnNextPage.Text = "Sau ▶";
            btnNextPage.UseVisualStyleBackColor = true;
            btnNextPage.Click += btnNextPage_Click;
            // 
            // FormProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvProducts);
            Controls.Add(pnlPager);
            Controls.Add(grpInfo);
            Controls.Add(pnlSearch);
            Font = new Font("Segoe UI", 10F);
            Name = "FormProducts";
            Padding = new Padding(12);
            Text = "Mặt hàng";
            Load += FormProducts_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCostPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlPager.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProducts;
        private GroupBox grpInfo;
        private Label lblCode;
        private TextBox txtCode;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblName;
        private TextBox txtName;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Label lblUnit;
        private TextBox txtUnit;
        private Label lblCostPrice;
        private NumericUpDown nudCostPrice;
        private Label lblSalePrice;
        private NumericUpDown nudSalePrice;
        private Label lblStock;
        private NumericUpDown nudStock;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkActive;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Panel pnlSearch;
        private TextBox txtKeyword;
        private ComboBox cboFilterCategory;
        private CheckBox chkShowInactive;
        private Button btnSearch;
        private Button btnReload;
        private Button btnExport;
        private Panel pnlPager;
        private Button btnPrevPage;
        private Label lblPage;
        private Button btnNextPage;
    }
}
