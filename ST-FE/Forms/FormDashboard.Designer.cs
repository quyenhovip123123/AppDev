namespace ST_FE.Forms
{
    partial class FormDashboard
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
            tlpBottom = new TableLayoutPanel();
            grpTopProducts = new GroupBox();
            dgvTopProducts = new DataGridView();
            grpLowStock = new GroupBox();
            dgvLowStock = new DataGridView();
            pnlChart = new Panel();
            flpCards = new FlowLayoutPanel();
            pnlCardRevenue = new Panel();
            lblRevenueTitle = new Label();
            lblRevenue = new Label();
            pnlCardProfit = new Panel();
            lblProfitTitle = new Label();
            lblProfit = new Label();
            pnlCardOrders = new Panel();
            lblOrdersTitle = new Label();
            lblOrders = new Label();
            pnlCardImport = new Panel();
            lblImportTitle = new Label();
            lblImport = new Label();
            pnlFilter = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            btnView = new Button();
            btnToday = new Button();
            btn7Days = new Button();
            btnThisMonth = new Button();
            tlpBottom.SuspendLayout();
            grpTopProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            grpLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).BeginInit();
            pnlChart.SuspendLayout();
            flpCards.SuspendLayout();
            pnlCardRevenue.SuspendLayout();
            pnlCardProfit.SuspendLayout();
            pnlCardOrders.SuspendLayout();
            pnlCardImport.SuspendLayout();
            pnlFilter.SuspendLayout();
            SuspendLayout();
            // 
            // tlpBottom
            // 
            tlpBottom.Controls.Add(grpTopProducts, 0, 0);
            tlpBottom.Controls.Add(grpLowStock, 1, 0);
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.ColumnCount = 2;
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Location = new Point(12, 432);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.Size = new Size(1026, 196);
            tlpBottom.TabIndex = 0;
            // 
            // grpTopProducts
            // 
            grpTopProducts.Controls.Add(dgvTopProducts);
            grpTopProducts.Dock = DockStyle.Fill;
            grpTopProducts.Location = new Point(3, 3);
            grpTopProducts.Name = "grpTopProducts";
            grpTopProducts.Size = new Size(507, 190);
            grpTopProducts.TabIndex = 0;
            grpTopProducts.Text = "Top 10 mặt hàng bán chạy";
            // 
            // dgvTopProducts
            // 
            dgvTopProducts.Dock = DockStyle.Fill;
            dgvTopProducts.Location = new Point(3, 21);
            dgvTopProducts.Name = "dgvTopProducts";
            dgvTopProducts.Size = new Size(501, 166);
            dgvTopProducts.TabIndex = 0;
            // 
            // grpLowStock
            // 
            grpLowStock.Controls.Add(dgvLowStock);
            grpLowStock.Dock = DockStyle.Fill;
            grpLowStock.Location = new Point(516, 3);
            grpLowStock.Name = "grpLowStock";
            grpLowStock.Size = new Size(507, 190);
            grpLowStock.TabIndex = 1;
            grpLowStock.Text = "Hàng sắp hết (tồn ≤ 10)";
            // 
            // dgvLowStock
            // 
            dgvLowStock.Dock = DockStyle.Fill;
            dgvLowStock.Location = new Point(3, 21);
            dgvLowStock.Name = "dgvLowStock";
            dgvLowStock.Size = new Size(501, 166);
            dgvLowStock.TabIndex = 0;
            // 
            // pnlChart
            // 
            pnlChart.BackColor = Color.White;
            pnlChart.Dock = DockStyle.Top;
            pnlChart.Location = new Point(12, 182);
            pnlChart.Name = "pnlChart";
            pnlChart.Size = new Size(1026, 250);
            pnlChart.TabIndex = 1;
            pnlChart.Paint += pnlChart_Paint;
            pnlChart.Resize += pnlChart_Resize;
            // 
            // flpCards
            // 
            flpCards.Controls.Add(pnlCardRevenue);
            flpCards.Controls.Add(pnlCardProfit);
            flpCards.Controls.Add(pnlCardOrders);
            flpCards.Controls.Add(pnlCardImport);
            flpCards.Dock = DockStyle.Top;
            flpCards.Location = new Point(12, 64);
            flpCards.Name = "flpCards";
            flpCards.Size = new Size(1026, 118);
            flpCards.TabIndex = 2;
            // 
            // pnlCardRevenue
            // 
            pnlCardRevenue.Controls.Add(lblRevenueTitle);
            pnlCardRevenue.Controls.Add(lblRevenue);
            pnlCardRevenue.BackColor = Color.FromArgb(22, 163, 74);
            pnlCardRevenue.Location = new Point(3, 3);
            pnlCardRevenue.Margin = new Padding(0, 0, 12, 12);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.Size = new Size(240, 100);
            pnlCardRevenue.TabIndex = 0;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.ForeColor = Color.White;
            lblRevenueTitle.Location = new Point(14, 12);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(216, 22);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "Doanh thu";
            // 
            // lblRevenue
            // 
            lblRevenue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblRevenue.ForeColor = Color.White;
            lblRevenue.Location = new Point(12, 42);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(220, 40);
            lblRevenue.TabIndex = 1;
            lblRevenue.Text = "0";
            // 
            // pnlCardProfit
            // 
            pnlCardProfit.Controls.Add(lblProfitTitle);
            pnlCardProfit.Controls.Add(lblProfit);
            pnlCardProfit.BackColor = Color.FromArgb(37, 99, 235);
            pnlCardProfit.Location = new Point(255, 3);
            pnlCardProfit.Margin = new Padding(0, 0, 12, 12);
            pnlCardProfit.Name = "pnlCardProfit";
            pnlCardProfit.Size = new Size(240, 100);
            pnlCardProfit.TabIndex = 1;
            // 
            // lblProfitTitle
            // 
            lblProfitTitle.ForeColor = Color.White;
            lblProfitTitle.Location = new Point(14, 12);
            lblProfitTitle.Name = "lblProfitTitle";
            lblProfitTitle.Size = new Size(216, 22);
            lblProfitTitle.TabIndex = 0;
            lblProfitTitle.Text = "Lợi nhuận gộp";
            // 
            // lblProfit
            // 
            lblProfit.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblProfit.ForeColor = Color.White;
            lblProfit.Location = new Point(12, 42);
            lblProfit.Name = "lblProfit";
            lblProfit.Size = new Size(220, 40);
            lblProfit.TabIndex = 1;
            lblProfit.Text = "0";
            // 
            // pnlCardOrders
            // 
            pnlCardOrders.Controls.Add(lblOrdersTitle);
            pnlCardOrders.Controls.Add(lblOrders);
            pnlCardOrders.BackColor = Color.FromArgb(245, 158, 11);
            pnlCardOrders.Location = new Point(507, 3);
            pnlCardOrders.Margin = new Padding(0, 0, 12, 12);
            pnlCardOrders.Name = "pnlCardOrders";
            pnlCardOrders.Size = new Size(240, 100);
            pnlCardOrders.TabIndex = 2;
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.ForeColor = Color.White;
            lblOrdersTitle.Location = new Point(14, 12);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Size = new Size(216, 22);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "Số hóa đơn";
            // 
            // lblOrders
            // 
            lblOrders.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblOrders.ForeColor = Color.White;
            lblOrders.Location = new Point(12, 42);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(220, 40);
            lblOrders.TabIndex = 1;
            lblOrders.Text = "0";
            // 
            // pnlCardImport
            // 
            pnlCardImport.Controls.Add(lblImportTitle);
            pnlCardImport.Controls.Add(lblImport);
            pnlCardImport.BackColor = Color.FromArgb(220, 38, 38);
            pnlCardImport.Location = new Point(759, 3);
            pnlCardImport.Margin = new Padding(0, 0, 12, 12);
            pnlCardImport.Name = "pnlCardImport";
            pnlCardImport.Size = new Size(240, 100);
            pnlCardImport.TabIndex = 3;
            // 
            // lblImportTitle
            // 
            lblImportTitle.ForeColor = Color.White;
            lblImportTitle.Location = new Point(14, 12);
            lblImportTitle.Name = "lblImportTitle";
            lblImportTitle.Size = new Size(216, 22);
            lblImportTitle.TabIndex = 0;
            lblImportTitle.Text = "Tiền nhập hàng";
            // 
            // lblImport
            // 
            lblImport.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblImport.ForeColor = Color.White;
            lblImport.Location = new Point(12, 42);
            lblImport.Name = "lblImport";
            lblImport.Size = new Size(220, 40);
            lblImport.TabIndex = 1;
            lblImport.Text = "0";
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(lblFrom);
            pnlFilter.Controls.Add(dtpFrom);
            pnlFilter.Controls.Add(lblTo);
            pnlFilter.Controls.Add(dtpTo);
            pnlFilter.Controls.Add(btnView);
            pnlFilter.Controls.Add(btnToday);
            pnlFilter.Controls.Add(btn7Days);
            pnlFilter.Controls.Add(btnThisMonth);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(12, 12);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1026, 52);
            pnlFilter.TabIndex = 3;
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
            // btnView
            // 
            btnView.Location = new Point(355, 5);
            btnView.Name = "btnView";
            btnView.Size = new Size(100, 32);
            btnView.TabIndex = 4;
            btnView.Text = "Xem";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += btnView_Click;
            // 
            // btnToday
            // 
            btnToday.Location = new Point(475, 5);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(100, 32);
            btnToday.TabIndex = 5;
            btnToday.Text = "Hôm nay";
            btnToday.UseVisualStyleBackColor = true;
            btnToday.Click += btnToday_Click;
            // 
            // btn7Days
            // 
            btn7Days.Location = new Point(585, 5);
            btn7Days.Name = "btn7Days";
            btn7Days.Size = new Size(100, 32);
            btn7Days.TabIndex = 6;
            btn7Days.Text = "7 ngày";
            btn7Days.UseVisualStyleBackColor = true;
            btn7Days.Click += btn7Days_Click;
            // 
            // btnThisMonth
            // 
            btnThisMonth.Location = new Point(695, 5);
            btnThisMonth.Name = "btnThisMonth";
            btnThisMonth.Size = new Size(100, 32);
            btnThisMonth.TabIndex = 7;
            btnThisMonth.Text = "Tháng này";
            btnThisMonth.UseVisualStyleBackColor = true;
            btnThisMonth.Click += btnThisMonth_Click;
            // 
            // FormDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1050, 640);
            Controls.Add(tlpBottom);
            Controls.Add(pnlChart);
            Controls.Add(flpCards);
            Controls.Add(pnlFilter);
            Font = new Font("Segoe UI", 10F);
            Name = "FormDashboard";
            Padding = new Padding(12);
            Text = "Tổng quan";
            Load += FormDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).EndInit();
            tlpBottom.ResumeLayout(false);
            tlpBottom.PerformLayout();
            grpTopProducts.ResumeLayout(false);
            grpTopProducts.PerformLayout();
            grpLowStock.ResumeLayout(false);
            grpLowStock.PerformLayout();
            pnlChart.ResumeLayout(false);
            pnlChart.PerformLayout();
            flpCards.ResumeLayout(false);
            flpCards.PerformLayout();
            pnlCardRevenue.ResumeLayout(false);
            pnlCardRevenue.PerformLayout();
            pnlCardProfit.ResumeLayout(false);
            pnlCardProfit.PerformLayout();
            pnlCardOrders.ResumeLayout(false);
            pnlCardOrders.PerformLayout();
            pnlCardImport.ResumeLayout(false);
            pnlCardImport.PerformLayout();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpBottom;
        private GroupBox grpTopProducts;
        private DataGridView dgvTopProducts;
        private GroupBox grpLowStock;
        private DataGridView dgvLowStock;
        private Panel pnlChart;
        private FlowLayoutPanel flpCards;
        private Panel pnlCardRevenue;
        private Label lblRevenueTitle;
        private Label lblRevenue;
        private Panel pnlCardProfit;
        private Label lblProfitTitle;
        private Label lblProfit;
        private Panel pnlCardOrders;
        private Label lblOrdersTitle;
        private Label lblOrders;
        private Panel pnlCardImport;
        private Label lblImportTitle;
        private Label lblImport;
        private Panel pnlFilter;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Button btnView;
        private Button btnToday;
        private Button btn7Days;
        private Button btnThisMonth;
    }
}
