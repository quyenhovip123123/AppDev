namespace ST_FE.Forms
{
    partial class FormOrders
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
            dgvOrders = new DataGridView();
            pnlFilter = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnViewDetail = new Button();
            btnCancelOrder = new Button();
            lblSummary = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            pnlFilter.SuspendLayout();
            SuspendLayout();
            // 
            // dgvOrders
            // 
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.Location = new Point(12, 64);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.Size = new Size(580, 500);
            dgvOrders.TabIndex = 0;
            dgvOrders.CellDoubleClick += dgvOrders_CellDoubleClick;
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(txtKeyword);
            pnlFilter.Controls.Add(btnSearch);
            pnlFilter.Controls.Add(btnViewDetail);
            pnlFilter.Controls.Add(btnCancelOrder);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(12, 12);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1026, 52);
            pnlFilter.TabIndex = 1;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(0, 11);
            lblFrom.Name = "lblFrom";
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Từ ngày";
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(60, 8);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(120, 25);
            dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(190, 11);
            lblTo.Name = "lblTo";
            lblTo.TabIndex = 2;
            lblTo.Text = "đến";
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(225, 8);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(120, 25);
            dtpTo.TabIndex = 3;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(355, 8);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Mã HĐ / tên khách...";
            txtKeyword.Size = new Size(200, 25);
            txtKeyword.TabIndex = 4;
            txtKeyword.KeyDown += txtKeyword_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(565, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 32);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "🔍 Lọc";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnViewDetail
            // 
            btnViewDetail.Location = new Point(675, 5);
            btnViewDetail.Name = "btnViewDetail";
            btnViewDetail.Size = new Size(120, 32);
            btnViewDetail.TabIndex = 6;
            btnViewDetail.Text = "Xem chi tiết";
            btnViewDetail.UseVisualStyleBackColor = true;
            btnViewDetail.Click += btnViewDetail_Click;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.Location = new Point(805, 5);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(120, 32);
            btnCancelOrder.TabIndex = 7;
            btnCancelOrder.Text = "Hủy hóa đơn";
            btnCancelOrder.UseVisualStyleBackColor = true;
            btnCancelOrder.Click += btnCancelOrder_Click;
            // 
            // lblSummary
            // 
            lblSummary.Dock = DockStyle.Bottom;
            lblSummary.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSummary.Location = new Point(12, 598);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(1026, 30);
            lblSummary.TabIndex = 2;
            lblSummary.Text = "";
            lblSummary.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormOrders
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(dgvOrders);
            Controls.Add(pnlFilter);
            Controls.Add(lblSummary);
            Font = new Font("Segoe UI", 10F);
            Name = "FormOrders";
            Padding = new Padding(12);
            Text = "Hóa đơn";
            Load += FormOrders_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOrders;
        private Panel pnlFilter;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnViewDetail;
        private Button btnCancelOrder;
        private Label lblSummary;
    }
}
