namespace ST_FE
{
    partial class FormPOS
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
            tableLayout = new TableLayoutPanel();
            panelLeft = new Panel();
            dgvCart = new DataGridView();
            panelScan = new Panel();
            txtBarcode = new TextBox();
            lblBarcode = new Label();
            panelRight = new Panel();
            btnClearCart = new Button();
            btnCheckout = new Button();
            lblChange = new Label();
            lblChangeCaption = new Label();
            txtCashReceived = new TextBox();
            lblCashCaption = new Label();
            lblTotalAmount = new Label();
            lblTotalCaption = new Label();
            lblCustomerName = new Label();
            txtCustomerPhone = new TextBox();
            lblPhoneCaption = new Label();
            tableLayout.SuspendLayout();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panelScan.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout  (Trái 65% giỏ hàng / Phải 35% thanh toán)
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayout.Controls.Add(panelLeft, 0, 0);
            tableLayout.Controls.Add(panelRight, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(10);
            tableLayout.RowCount = 1;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.TabIndex = 0;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(dgvCart);
            panelLeft.Controls.Add(panelScan);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Name = "panelLeft";
            panelLeft.TabIndex = 0;
            // 
            // panelScan
            // 
            panelScan.BackColor = Color.White;
            panelScan.Controls.Add(txtBarcode);
            panelScan.Controls.Add(lblBarcode);
            panelScan.Dock = DockStyle.Top;
            panelScan.Name = "panelScan";
            panelScan.Size = new Size(600, 60);
            panelScan.TabIndex = 0;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBarcode.Location = new Point(10, 18);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Text = "Quét mã vạch:";
            // 
            // txtBarcode
            // 
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(140, 14);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Quét hoặc nhập mã vạch rồi nhấn Enter...";
            txtBarcode.Size = new Size(440, 29);
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.ColumnHeadersHeight = 36;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersVisible = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.TabIndex = 1;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(btnClearCart);
            panelRight.Controls.Add(btnCheckout);
            panelRight.Controls.Add(lblChange);
            panelRight.Controls.Add(lblChangeCaption);
            panelRight.Controls.Add(txtCashReceived);
            panelRight.Controls.Add(lblCashCaption);
            panelRight.Controls.Add(lblTotalAmount);
            panelRight.Controls.Add(lblTotalCaption);
            panelRight.Controls.Add(lblCustomerName);
            panelRight.Controls.Add(txtCustomerPhone);
            panelRight.Controls.Add(lblPhoneCaption);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(15);
            panelRight.TabIndex = 1;
            // 
            // lblPhoneCaption
            // 
            lblPhoneCaption.AutoSize = true;
            lblPhoneCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhoneCaption.Location = new Point(15, 15);
            lblPhoneCaption.Name = "lblPhoneCaption";
            lblPhoneCaption.Text = "SĐT khách hàng thân thiết:";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerPhone.Font = new Font("Segoe UI", 11F);
            txtCustomerPhone.Location = new Point(15, 40);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT rồi nhấn Enter";
            txtCustomerPhone.Size = new Size(300, 27);
            txtCustomerPhone.TabIndex = 2;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;
            // 
            // lblCustomerName
            // 
            lblCustomerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            lblCustomerName.Location = new Point(15, 75);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(300, 40);
            lblCustomerName.Text = "Khách vãng lai";
            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCaption.Location = new Point(15, 125);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Text = "TỔNG TIỀN CẦN THANH TOÁN:";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalAmount.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotalAmount.Location = new Point(15, 150);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(300, 50);
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCashCaption
            // 
            lblCashCaption.AutoSize = true;
            lblCashCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCashCaption.Location = new Point(15, 215);
            lblCashCaption.Name = "lblCashCaption";
            lblCashCaption.Text = "Tiền khách đưa:";
            // 
            // txtCashReceived
            // 
            txtCashReceived.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCashReceived.Font = new Font("Segoe UI", 14F);
            txtCashReceived.Location = new Point(15, 240);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.Size = new Size(300, 32);
            txtCashReceived.TabIndex = 3;
            txtCashReceived.TextAlign = HorizontalAlignment.Right;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;
            // 
            // lblChangeCaption
            // 
            lblChangeCaption.AutoSize = true;
            lblChangeCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChangeCaption.Location = new Point(15, 285);
            lblChangeCaption.Name = "lblChangeCaption";
            lblChangeCaption.Text = "Tiền thừa trả khách:";
            // 
            // lblChange
            // 
            lblChange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblChange.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblChange.Location = new Point(15, 310);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(300, 35);
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCheckout
            // 
            btnCheckout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCheckout.BackColor = Color.FromArgb(25, 135, 84);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(15, 365);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(300, 60);
            btnCheckout.TabIndex = 4;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnClearCart.BackColor = Color.FromArgb(220, 53, 69);
            btnClearCart.Cursor = Cursors.Hand;
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(15, 435);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(300, 45);
            btnClearCart.TabIndex = 5;
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1034, 621);
            Controls.Add(tableLayout);
            Name = "FormPOS";
            Text = "Bán hàng (POS)";
            Load += FormPOS_Load;
            tableLayout.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            panelScan.ResumeLayout(false);
            panelScan.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Panel panelLeft;
        private Panel panelScan;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private DataGridView dgvCart;
        private Panel panelRight;
        private Label lblPhoneCaption;
        private TextBox txtCustomerPhone;
        private Label lblCustomerName;
        private Label lblTotalCaption;
        private Label lblTotalAmount;
        private Label lblCashCaption;
        private TextBox txtCashReceived;
        private Label lblChangeCaption;
        private Label lblChange;
        private Button btnCheckout;
        private Button btnClearCart;
    }
}
