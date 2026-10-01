namespace ST_FE.Forms
{
    partial class FormImports
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
            tabMain = new TabControl();
            tabCreate = new TabPage();
            dgvLines = new DataGridView();
            pnlImportInput = new Panel();
            lblProduct = new Label();
            cboProduct = new ComboBox();
            lblImportQty = new Label();
            nudImportQty = new NumericUpDown();
            lblUnitCost = new Label();
            nudUnitCost = new NumericUpDown();
            btnAddLine = new Button();
            lblSupplier = new Label();
            txtSupplier = new TextBox();
            lblImportNote = new Label();
            txtImportNote = new TextBox();
            pnlImportFooter = new Panel();
            btnRemoveLine = new Button();
            lblImportTotal = new Label();
            btnSaveImport = new Button();
            tabHistory = new TabPage();
            dgvReceipts = new DataGridView();
            dgvReceiptDetails = new DataGridView();
            pnlHistoryFilter = new Panel();
            lblHistoryFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblHistoryTo = new Label();
            dtpTo = new DateTimePicker();
            btnLoadHistory = new Button();
            tabMain.SuspendLayout();
            tabCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            pnlImportInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudImportQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitCost).BeginInit();
            pnlImportFooter.SuspendLayout();
            tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReceipts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReceiptDetails).BeginInit();
            pnlHistoryFilter.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabCreate);
            tabMain.Controls.Add(tabHistory);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(12, 12);
            tabMain.Name = "tabMain";
            tabMain.Size = new Size(1026, 616);
            tabMain.TabIndex = 0;
            // 
            // tabCreate
            // 
            tabCreate.Controls.Add(dgvLines);
            tabCreate.Controls.Add(pnlImportInput);
            tabCreate.Controls.Add(pnlImportFooter);
            tabCreate.Location = new Point(4, 26);
            tabCreate.Name = "tabCreate";
            tabCreate.Padding = new Padding(8);
            tabCreate.Size = new Size(1018, 586);
            tabCreate.TabIndex = 0;
            tabCreate.Text = "Lập phiếu nhập";
            tabCreate.UseVisualStyleBackColor = true;
            // 
            // dgvLines
            // 
            dgvLines.Dock = DockStyle.Fill;
            dgvLines.Location = new Point(8, 108);
            dgvLines.Name = "dgvLines";
            dgvLines.Size = new Size(1002, 410);
            dgvLines.TabIndex = 0;
            // 
            // pnlImportInput
            // 
            pnlImportInput.Controls.Add(lblProduct);
            pnlImportInput.Controls.Add(cboProduct);
            pnlImportInput.Controls.Add(lblImportQty);
            pnlImportInput.Controls.Add(nudImportQty);
            pnlImportInput.Controls.Add(lblUnitCost);
            pnlImportInput.Controls.Add(nudUnitCost);
            pnlImportInput.Controls.Add(btnAddLine);
            pnlImportInput.Controls.Add(lblSupplier);
            pnlImportInput.Controls.Add(txtSupplier);
            pnlImportInput.Controls.Add(lblImportNote);
            pnlImportInput.Controls.Add(txtImportNote);
            pnlImportInput.Dock = DockStyle.Top;
            pnlImportInput.Location = new Point(8, 8);
            pnlImportInput.Name = "pnlImportInput";
            pnlImportInput.Size = new Size(1002, 100);
            pnlImportInput.TabIndex = 1;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(0, 11);
            lblProduct.Name = "lblProduct";
            lblProduct.TabIndex = 0;
            lblProduct.Text = "Mặt hàng";
            // 
            // cboProduct
            // 
            cboProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboProduct.Location = new Point(75, 8);
            cboProduct.Name = "cboProduct";
            cboProduct.Size = new Size(340, 25);
            cboProduct.TabIndex = 1;
            cboProduct.SelectedIndexChanged += cboProduct_SelectedIndexChanged;
            // 
            // lblImportQty
            // 
            lblImportQty.AutoSize = true;
            lblImportQty.Location = new Point(425, 11);
            lblImportQty.Name = "lblImportQty";
            lblImportQty.TabIndex = 2;
            lblImportQty.Text = "Số lượng";
            // 
            // nudImportQty
            // 
            nudImportQty.Location = new Point(490, 8);
            nudImportQty.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudImportQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudImportQty.Name = "nudImportQty";
            nudImportQty.Size = new Size(90, 25);
            nudImportQty.TabIndex = 3;
            nudImportQty.ThousandsSeparator = true;
            nudImportQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblUnitCost
            // 
            lblUnitCost.AutoSize = true;
            lblUnitCost.Location = new Point(590, 11);
            lblUnitCost.Name = "lblUnitCost";
            lblUnitCost.TabIndex = 4;
            lblUnitCost.Text = "Giá nhập";
            // 
            // nudUnitCost
            // 
            nudUnitCost.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudUnitCost.Location = new Point(655, 8);
            nudUnitCost.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudUnitCost.Name = "nudUnitCost";
            nudUnitCost.Size = new Size(130, 25);
            nudUnitCost.TabIndex = 5;
            nudUnitCost.ThousandsSeparator = true;
            // 
            // btnAddLine
            // 
            btnAddLine.Location = new Point(795, 5);
            btnAddLine.Name = "btnAddLine";
            btnAddLine.Size = new Size(130, 32);
            btnAddLine.TabIndex = 6;
            btnAddLine.Text = "➕ Thêm dòng";
            btnAddLine.UseVisualStyleBackColor = true;
            btnAddLine.Click += btnAddLine_Click;
            // 
            // lblSupplier
            // 
            lblSupplier.AutoSize = true;
            lblSupplier.Location = new Point(0, 56);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.TabIndex = 7;
            lblSupplier.Text = "Nhà CC *";
            // 
            // txtSupplier
            // 
            txtSupplier.Location = new Point(75, 53);
            txtSupplier.MaxLength = 150;
            txtSupplier.Name = "txtSupplier";
            txtSupplier.Size = new Size(340, 25);
            txtSupplier.TabIndex = 8;
            // 
            // lblImportNote
            // 
            lblImportNote.AutoSize = true;
            lblImportNote.Location = new Point(425, 56);
            lblImportNote.Name = "lblImportNote";
            lblImportNote.TabIndex = 9;
            lblImportNote.Text = "Ghi chú";
            // 
            // txtImportNote
            // 
            txtImportNote.Location = new Point(490, 53);
            txtImportNote.MaxLength = 255;
            txtImportNote.Name = "txtImportNote";
            txtImportNote.Size = new Size(435, 25);
            txtImportNote.TabIndex = 10;
            // 
            // pnlImportFooter
            // 
            pnlImportFooter.Controls.Add(btnRemoveLine);
            pnlImportFooter.Controls.Add(lblImportTotal);
            pnlImportFooter.Controls.Add(btnSaveImport);
            pnlImportFooter.Dock = DockStyle.Bottom;
            pnlImportFooter.Location = new Point(8, 518);
            pnlImportFooter.Name = "pnlImportFooter";
            pnlImportFooter.Size = new Size(1002, 60);
            pnlImportFooter.TabIndex = 2;
            // 
            // btnRemoveLine
            // 
            btnRemoveLine.Location = new Point(0, 12);
            btnRemoveLine.Name = "btnRemoveLine";
            btnRemoveLine.Size = new Size(110, 36);
            btnRemoveLine.TabIndex = 0;
            btnRemoveLine.Text = "Xóa dòng";
            btnRemoveLine.UseVisualStyleBackColor = true;
            btnRemoveLine.Click += btnRemoveLine_Click;
            // 
            // lblImportTotal
            // 
            lblImportTotal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblImportTotal.ForeColor = Color.FromArgb(217, 119, 6);
            lblImportTotal.Location = new Point(330, 12);
            lblImportTotal.Name = "lblImportTotal";
            lblImportTotal.Size = new Size(450, 36);
            lblImportTotal.TabIndex = 1;
            lblImportTotal.Text = "Tổng tiền: 0 đ";
            lblImportTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSaveImport
            // 
            btnSaveImport.Location = new Point(795, 8);
            btnSaveImport.Name = "btnSaveImport";
            btnSaveImport.Size = new Size(200, 44);
            btnSaveImport.TabIndex = 2;
            btnSaveImport.Text = "💾 LƯU PHIẾU NHẬP";
            btnSaveImport.UseVisualStyleBackColor = true;
            btnSaveImport.Click += btnSaveImport_Click;
            // 
            // tabHistory
            // 
            tabHistory.Controls.Add(dgvReceipts);
            tabHistory.Controls.Add(dgvReceiptDetails);
            tabHistory.Controls.Add(pnlHistoryFilter);
            tabHistory.Location = new Point(4, 26);
            tabHistory.Name = "tabHistory";
            tabHistory.Padding = new Padding(8);
            tabHistory.Size = new Size(1018, 586);
            tabHistory.TabIndex = 1;
            tabHistory.Text = "Lịch sử nhập kho";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvReceipts
            // 
            dgvReceipts.Dock = DockStyle.Fill;
            dgvReceipts.Location = new Point(8, 58);
            dgvReceipts.Name = "dgvReceipts";
            dgvReceipts.Size = new Size(1002, 320);
            dgvReceipts.TabIndex = 0;
            dgvReceipts.SelectionChanged += dgvReceipts_SelectionChanged;
            // 
            // dgvReceiptDetails
            // 
            dgvReceiptDetails.Dock = DockStyle.Bottom;
            dgvReceiptDetails.Location = new Point(8, 378);
            dgvReceiptDetails.Name = "dgvReceiptDetails";
            dgvReceiptDetails.Size = new Size(1002, 200);
            dgvReceiptDetails.TabIndex = 1;
            // 
            // pnlHistoryFilter
            // 
            pnlHistoryFilter.Controls.Add(lblHistoryFrom);
            pnlHistoryFilter.Controls.Add(dtpFrom);
            pnlHistoryFilter.Controls.Add(lblHistoryTo);
            pnlHistoryFilter.Controls.Add(dtpTo);
            pnlHistoryFilter.Controls.Add(btnLoadHistory);
            pnlHistoryFilter.Dock = DockStyle.Top;
            pnlHistoryFilter.Location = new Point(8, 8);
            pnlHistoryFilter.Name = "pnlHistoryFilter";
            pnlHistoryFilter.Size = new Size(1002, 50);
            pnlHistoryFilter.TabIndex = 2;
            // 
            // lblHistoryFrom
            // 
            lblHistoryFrom.AutoSize = true;
            lblHistoryFrom.Location = new Point(0, 11);
            lblHistoryFrom.Name = "lblHistoryFrom";
            lblHistoryFrom.TabIndex = 0;
            lblHistoryFrom.Text = "Từ ngày";
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(60, 8);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(120, 25);
            dtpFrom.TabIndex = 1;
            // 
            // lblHistoryTo
            // 
            lblHistoryTo.AutoSize = true;
            lblHistoryTo.Location = new Point(190, 11);
            lblHistoryTo.Name = "lblHistoryTo";
            lblHistoryTo.TabIndex = 2;
            lblHistoryTo.Text = "đến";
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(225, 8);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(120, 25);
            dtpTo.TabIndex = 3;
            // 
            // btnLoadHistory
            // 
            btnLoadHistory.Location = new Point(355, 5);
            btnLoadHistory.Name = "btnLoadHistory";
            btnLoadHistory.Size = new Size(100, 32);
            btnLoadHistory.TabIndex = 4;
            btnLoadHistory.Text = "🔍 Lọc";
            btnLoadHistory.UseVisualStyleBackColor = true;
            btnLoadHistory.Click += btnLoadHistory_Click;
            // 
            // FormImports
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(tabMain);
            Font = new Font("Segoe UI", 10F);
            Name = "FormImports";
            Padding = new Padding(12);
            Text = "Nhập kho";
            Load += FormImports_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudImportQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudUnitCost).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReceipts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReceiptDetails).EndInit();
            tabMain.ResumeLayout(false);
            tabMain.PerformLayout();
            tabCreate.ResumeLayout(false);
            tabCreate.PerformLayout();
            pnlImportInput.ResumeLayout(false);
            pnlImportInput.PerformLayout();
            pnlImportFooter.ResumeLayout(false);
            pnlImportFooter.PerformLayout();
            tabHistory.ResumeLayout(false);
            tabHistory.PerformLayout();
            pnlHistoryFilter.ResumeLayout(false);
            pnlHistoryFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabCreate;
        private DataGridView dgvLines;
        private Panel pnlImportInput;
        private Label lblProduct;
        private ComboBox cboProduct;
        private Label lblImportQty;
        private NumericUpDown nudImportQty;
        private Label lblUnitCost;
        private NumericUpDown nudUnitCost;
        private Button btnAddLine;
        private Label lblSupplier;
        private TextBox txtSupplier;
        private Label lblImportNote;
        private TextBox txtImportNote;
        private Panel pnlImportFooter;
        private Button btnRemoveLine;
        private Label lblImportTotal;
        private Button btnSaveImport;
        private TabPage tabHistory;
        private DataGridView dgvReceipts;
        private DataGridView dgvReceiptDetails;
        private Panel pnlHistoryFilter;
        private Label lblHistoryFrom;
        private DateTimePicker dtpFrom;
        private Label lblHistoryTo;
        private DateTimePicker dtpTo;
        private Button btnLoadHistory;
    }
}
