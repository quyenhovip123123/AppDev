namespace ST_FE.Forms
{
    partial class FormSales
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
            pnlProductSearch = new Panel();
            txtProductSearch = new TextBox();
            lblQuantity = new Label();
            nudQuantity = new NumericUpDown();
            btnAddToCart = new Button();
            pnlCart = new Panel();
            dgvCart = new DataGridView();
            pnlCartActions = new Panel();
            btnIncrease = new Button();
            btnDecrease = new Button();
            btnRemoveItem = new Button();
            btnClearCart = new Button();
            lblCartTitle = new Label();
            pnlCheckout = new Panel();
            lblCustomerPhone = new Label();
            txtCustomerPhone = new TextBox();
            btnFindCustomer = new Button();
            btnRetailCustomer = new Button();
            lblCustomerInfo = new Label();
            lblSubTotalCaption = new Label();
            lblSubTotal = new Label();
            lblDiscountCaption = new Label();
            nudDiscount = new NumericUpDown();
            lblTotalCaption = new Label();
            lblTotal = new Label();
            lblPaidCaption = new Label();
            btnExactCash = new Button();
            nudCustomerPaid = new NumericUpDown();
            lblChangeCaption = new Label();
            lblChange = new Label();
            txtNote = new TextBox();
            btnCheckout = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            pnlProductSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlCartActions.SuspendLayout();
            pnlCheckout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCustomerPaid).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(12, 64);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(580, 500);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
            // 
            // pnlProductSearch
            // 
            pnlProductSearch.Controls.Add(txtProductSearch);
            pnlProductSearch.Controls.Add(lblQuantity);
            pnlProductSearch.Controls.Add(nudQuantity);
            pnlProductSearch.Controls.Add(btnAddToCart);
            pnlProductSearch.Dock = DockStyle.Top;
            pnlProductSearch.Location = new Point(12, 12);
            pnlProductSearch.Name = "pnlProductSearch";
            pnlProductSearch.Size = new Size(560, 52);
            pnlProductSearch.TabIndex = 1;
            // 
            // txtProductSearch
            // 
            txtProductSearch.Location = new Point(0, 8);
            txtProductSearch.Name = "txtProductSearch";
            txtProductSearch.PlaceholderText = "Nhập mã hoặc tên hàng rồi Enter...";
            txtProductSearch.Size = new Size(300, 25);
            txtProductSearch.TabIndex = 0;
            txtProductSearch.KeyDown += txtProductSearch_KeyDown;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(312, 11);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.TabIndex = 1;
            lblQuantity.Text = "SL:";
            // 
            // nudQuantity
            // 
            nudQuantity.Location = new Point(345, 8);
            nudQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(70, 25);
            nudQuantity.TabIndex = 2;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddToCart
            // 
            btnAddToCart.Location = new Point(425, 5);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(140, 32);
            btnAddToCart.TabIndex = 3;
            btnAddToCart.Text = "Thêm vào giỏ ➜";
            btnAddToCart.UseVisualStyleBackColor = true;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // pnlCart
            // 
            pnlCart.Controls.Add(dgvCart);
            pnlCart.Controls.Add(pnlCartActions);
            pnlCart.Controls.Add(lblCartTitle);
            pnlCart.Controls.Add(pnlCheckout);
            pnlCart.Dock = DockStyle.Right;
            pnlCart.Location = new Point(578, 12);
            pnlCart.Name = "pnlCart";
            pnlCart.Padding = new Padding(12, 0, 0, 0);
            pnlCart.Size = new Size(460, 616);
            pnlCart.TabIndex = 2;
            // 
            // dgvCart
            // 
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(12, 74);
            dgvCart.Name = "dgvCart";
            dgvCart.Size = new Size(448, 200);
            dgvCart.TabIndex = 0;
            // 
            // pnlCartActions
            // 
            pnlCartActions.Controls.Add(btnIncrease);
            pnlCartActions.Controls.Add(btnDecrease);
            pnlCartActions.Controls.Add(btnRemoveItem);
            pnlCartActions.Controls.Add(btnClearCart);
            pnlCartActions.Dock = DockStyle.Top;
            pnlCartActions.Location = new Point(12, 32);
            pnlCartActions.Name = "pnlCartActions";
            pnlCartActions.Size = new Size(448, 42);
            pnlCartActions.TabIndex = 1;
            // 
            // btnIncrease
            // 
            btnIncrease.Location = new Point(0, 4);
            btnIncrease.Name = "btnIncrease";
            btnIncrease.Size = new Size(60, 32);
            btnIncrease.TabIndex = 0;
            btnIncrease.Text = "+1";
            btnIncrease.UseVisualStyleBackColor = true;
            btnIncrease.Click += btnIncrease_Click;
            // 
            // btnDecrease
            // 
            btnDecrease.Location = new Point(66, 4);
            btnDecrease.Name = "btnDecrease";
            btnDecrease.Size = new Size(60, 32);
            btnDecrease.TabIndex = 1;
            btnDecrease.Text = "-1";
            btnDecrease.UseVisualStyleBackColor = true;
            btnDecrease.Click += btnDecrease_Click;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Location = new Point(132, 4);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(100, 32);
            btnRemoveItem.TabIndex = 2;
            btnRemoveItem.Text = "Xóa dòng";
            btnRemoveItem.UseVisualStyleBackColor = true;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.Location = new Point(238, 4);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(100, 32);
            btnClearCart.TabIndex = 3;
            btnClearCart.Text = "Xóa giỏ";
            btnClearCart.UseVisualStyleBackColor = true;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // lblCartTitle
            // 
            lblCartTitle.Dock = DockStyle.Top;
            lblCartTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCartTitle.Location = new Point(12, 0);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(448, 32);
            lblCartTitle.TabIndex = 2;
            lblCartTitle.Text = "🛒 Giỏ hàng";
            lblCartTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlCheckout
            // 
            pnlCheckout.Controls.Add(lblCustomerPhone);
            pnlCheckout.Controls.Add(txtCustomerPhone);
            pnlCheckout.Controls.Add(btnFindCustomer);
            pnlCheckout.Controls.Add(btnRetailCustomer);
            pnlCheckout.Controls.Add(lblCustomerInfo);
            pnlCheckout.Controls.Add(lblSubTotalCaption);
            pnlCheckout.Controls.Add(lblSubTotal);
            pnlCheckout.Controls.Add(lblDiscountCaption);
            pnlCheckout.Controls.Add(nudDiscount);
            pnlCheckout.Controls.Add(lblTotalCaption);
            pnlCheckout.Controls.Add(lblTotal);
            pnlCheckout.Controls.Add(lblPaidCaption);
            pnlCheckout.Controls.Add(btnExactCash);
            pnlCheckout.Controls.Add(nudCustomerPaid);
            pnlCheckout.Controls.Add(lblChangeCaption);
            pnlCheckout.Controls.Add(lblChange);
            pnlCheckout.Controls.Add(txtNote);
            pnlCheckout.Controls.Add(btnCheckout);
            pnlCheckout.BackColor = Color.White;
            pnlCheckout.Dock = DockStyle.Bottom;
            pnlCheckout.Location = new Point(12, 276);
            pnlCheckout.Name = "pnlCheckout";
            pnlCheckout.Size = new Size(448, 340);
            pnlCheckout.TabIndex = 3;
            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Location = new Point(10, 13);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.TabIndex = 0;
            lblCustomerPhone.Text = "SĐT khách:";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Location = new Point(100, 10);
            txtCustomerPhone.MaxLength = 11;
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new Size(140, 25);
            txtCustomerPhone.TabIndex = 1;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;
            // 
            // btnFindCustomer
            // 
            btnFindCustomer.Location = new Point(246, 8);
            btnFindCustomer.Name = "btnFindCustomer";
            btnFindCustomer.Size = new Size(80, 29);
            btnFindCustomer.TabIndex = 2;
            btnFindCustomer.Text = "Tìm";
            btnFindCustomer.UseVisualStyleBackColor = true;
            btnFindCustomer.Click += btnFindCustomer_Click;
            // 
            // btnRetailCustomer
            // 
            btnRetailCustomer.Location = new Point(332, 8);
            btnRetailCustomer.Name = "btnRetailCustomer";
            btnRetailCustomer.Size = new Size(106, 29);
            btnRetailCustomer.TabIndex = 3;
            btnRetailCustomer.Text = "Khách lẻ";
            btnRetailCustomer.UseVisualStyleBackColor = true;
            btnRetailCustomer.Click += btnRetailCustomer_Click;
            // 
            // lblCustomerInfo
            // 
            lblCustomerInfo.ForeColor = Color.FromArgb(37, 99, 235);
            lblCustomerInfo.Location = new Point(10, 42);
            lblCustomerInfo.Name = "lblCustomerInfo";
            lblCustomerInfo.Size = new Size(428, 22);
            lblCustomerInfo.TabIndex = 4;
            lblCustomerInfo.Text = "Khách lẻ";
            // 
            // lblSubTotalCaption
            // 
            lblSubTotalCaption.AutoSize = true;
            lblSubTotalCaption.Location = new Point(10, 75);
            lblSubTotalCaption.Name = "lblSubTotalCaption";
            lblSubTotalCaption.TabIndex = 5;
            lblSubTotalCaption.Text = "Tổng tiền hàng:";
            // 
            // lblSubTotal
            // 
            lblSubTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSubTotal.Location = new Point(200, 72);
            lblSubTotal.Name = "lblSubTotal";
            lblSubTotal.Size = new Size(238, 25);
            lblSubTotal.TabIndex = 6;
            lblSubTotal.Text = "0 đ";
            lblSubTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            lblDiscountCaption.AutoSize = true;
            lblDiscountCaption.Location = new Point(10, 108);
            lblDiscountCaption.Name = "lblDiscountCaption";
            lblDiscountCaption.TabIndex = 7;
            lblDiscountCaption.Text = "Giảm giá (đ):";
            // 
            // nudDiscount
            // 
            nudDiscount.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudDiscount.Location = new Point(260, 105);
            nudDiscount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudDiscount.Name = "nudDiscount";
            nudDiscount.Size = new Size(178, 25);
            nudDiscount.TabIndex = 8;
            nudDiscount.TextAlign = HorizontalAlignment.Right;
            nudDiscount.ThousandsSeparator = true;
            nudDiscount.ValueChanged += Totals_ValueChanged;
            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalCaption.Location = new Point(10, 145);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.TabIndex = 9;
            lblTotalCaption.Text = "KHÁCH CẦN TRẢ:";
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(217, 119, 6);
            lblTotal.Location = new Point(180, 138);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(258, 34);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "0 đ";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPaidCaption
            // 
            lblPaidCaption.AutoSize = true;
            lblPaidCaption.Location = new Point(10, 185);
            lblPaidCaption.Name = "lblPaidCaption";
            lblPaidCaption.TabIndex = 11;
            lblPaidCaption.Text = "Khách đưa (đ):";
            // 
            // btnExactCash
            // 
            btnExactCash.Location = new Point(150, 181);
            btnExactCash.Name = "btnExactCash";
            btnExactCash.Size = new Size(100, 29);
            btnExactCash.TabIndex = 12;
            btnExactCash.Text = "Đủ tiền";
            btnExactCash.UseVisualStyleBackColor = true;
            btnExactCash.Click += btnExactCash_Click;
            // 
            // nudCustomerPaid
            // 
            nudCustomerPaid.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCustomerPaid.Location = new Point(260, 182);
            nudCustomerPaid.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudCustomerPaid.Name = "nudCustomerPaid";
            nudCustomerPaid.Size = new Size(178, 25);
            nudCustomerPaid.TabIndex = 13;
            nudCustomerPaid.TextAlign = HorizontalAlignment.Right;
            nudCustomerPaid.ThousandsSeparator = true;
            nudCustomerPaid.ValueChanged += Totals_ValueChanged;
            // 
            // lblChangeCaption
            // 
            lblChangeCaption.AutoSize = true;
            lblChangeCaption.Location = new Point(10, 220);
            lblChangeCaption.Name = "lblChangeCaption";
            lblChangeCaption.TabIndex = 14;
            lblChangeCaption.Text = "Tiền thừa trả khách:";
            // 
            // lblChange
            // 
            lblChange.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChange.ForeColor = Color.FromArgb(22, 163, 74);
            lblChange.Location = new Point(200, 217);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(238, 25);
            lblChange.TabIndex = 15;
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNote
            // 
            txtNote.Location = new Point(10, 250);
            txtNote.MaxLength = 255;
            txtNote.Name = "txtNote";
            txtNote.PlaceholderText = "Ghi chú hóa đơn...";
            txtNote.Size = new Size(428, 25);
            txtNote.TabIndex = 16;
            // 
            // btnCheckout
            // 
            btnCheckout.Location = new Point(10, 284);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(428, 48);
            btnCheckout.TabIndex = 17;
            btnCheckout.Text = "💳  THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = true;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // FormSales
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvProducts);
            Controls.Add(pnlProductSearch);
            Controls.Add(pnlCart);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            Name = "FormSales";
            Padding = new Padding(12);
            Text = "Bán hàng";
            Load += FormSales_Load;
            KeyDown += FormSales_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCustomerPaid).EndInit();
            pnlProductSearch.ResumeLayout(false);
            pnlProductSearch.PerformLayout();
            pnlCart.ResumeLayout(false);
            pnlCart.PerformLayout();
            pnlCartActions.ResumeLayout(false);
            pnlCartActions.PerformLayout();
            pnlCheckout.ResumeLayout(false);
            pnlCheckout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProducts;
        private Panel pnlProductSearch;
        private TextBox txtProductSearch;
        private Label lblQuantity;
        private NumericUpDown nudQuantity;
        private Button btnAddToCart;
        private Panel pnlCart;
        private DataGridView dgvCart;
        private Panel pnlCartActions;
        private Button btnIncrease;
        private Button btnDecrease;
        private Button btnRemoveItem;
        private Button btnClearCart;
        private Label lblCartTitle;
        private Panel pnlCheckout;
        private Label lblCustomerPhone;
        private TextBox txtCustomerPhone;
        private Button btnFindCustomer;
        private Button btnRetailCustomer;
        private Label lblCustomerInfo;
        private Label lblSubTotalCaption;
        private Label lblSubTotal;
        private Label lblDiscountCaption;
        private NumericUpDown nudDiscount;
        private Label lblTotalCaption;
        private Label lblTotal;
        private Label lblPaidCaption;
        private Button btnExactCash;
        private NumericUpDown nudCustomerPaid;
        private Label lblChangeCaption;
        private Label lblChange;
        private TextBox txtNote;
        private Button btnCheckout;
    }
}
