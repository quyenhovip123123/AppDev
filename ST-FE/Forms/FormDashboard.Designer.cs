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
            chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            flpCards = new FlowLayoutPanel();
            pnlCardTodayRevenue = new Panel();
            lblTodayRevenueTitle = new Label();
            lblTodayRevenue = new Label();
            pnlCardTodayOrders = new Panel();
            lblTodayOrdersTitle = new Label();
            lblTodayOrders = new Label();
            pnlCardNewCustomers = new Panel();
            lblNewCustomersTitle = new Label();
            lblNewCustomers = new Label();
            pnlCardLowStock = new Panel();
            lblLowStockTitle = new Label();
            lblLowStock = new Label();
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
            ((System.ComponentModel.ISupportInitialize)chartRevenue).BeginInit();
            flpCards.SuspendLayout();
            pnlCardTodayRevenue.SuspendLayout();
            pnlCardTodayOrders.SuspendLayout();
            pnlCardNewCustomers.SuspendLayout();
            pnlCardLowStock.SuspendLayout();
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
            grpTopProducts.Text = "Top 5 mặt hàng bán chạy";
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
            // chartRevenue
            // 
            chartRevenue.BackColor = Color.White;
            chartRevenue.Dock = DockStyle.Top;
            chartRevenue.Location = new Point(12, 182);
            chartRevenue.Name = "chartRevenue";
            chartRevenue.Size = new Size(1026, 250);
            chartRevenue.TabIndex = 1;
            // 
            // flpCards
            // 
            flpCards.Controls.Add(pnlCardTodayRevenue);
            flpCards.Controls.Add(pnlCardTodayOrders);
            flpCards.Controls.Add(pnlCardNewCustomers);
            flpCards.Controls.Add(pnlCardLowStock);
            flpCards.Dock = DockStyle.Top;
            flpCards.Location = new Point(12, 64);
            flpCards.Name = "flpCards";
            flpCards.Size = new Size(1026, 118);
            flpCards.TabIndex = 2;
            // 
            // pnlCardTodayRevenue
            // 
            pnlCardTodayRevenue.Controls.Add(lblTodayRevenueTitle);
            pnlCardTodayRevenue.Controls.Add(lblTodayRevenue);
            pnlCardTodayRevenue.BackColor = Color.FromArgb(22, 163, 74);
            pnlCardTodayRevenue.Location = new Point(3, 3);
            pnlCardTodayRevenue.Margin = new Padding(0, 0, 12, 12);
            pnlCardTodayRevenue.Name = "pnlCardTodayRevenue";
            pnlCardTodayRevenue.Size = new Size(240, 100);
            pnlCardTodayRevenue.TabIndex = 0;
            // 
            // lblTodayRevenueTitle
            // 
            lblTodayRevenueTitle.ForeColor = Color.White;
            lblTodayRevenueTitle.Location = new Point(14, 12);
            lblTodayRevenueTitle.Name = "lblTodayRevenueTitle";
            lblTodayRevenueTitle.Size = new Size(216, 22);
            lblTodayRevenueTitle.TabIndex = 0;
            lblTodayRevenueTitle.Text = "Doanh thu hôm nay";
            // 
            // lblTodayRevenue
            // 
            lblTodayRevenue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTodayRevenue.ForeColor = Color.White;
            lblTodayRevenue.Location = new Point(12, 42);
            lblTodayRevenue.Name = "lblTodayRevenue";
            lblTodayRevenue.Size = new Size(220, 40);
            lblTodayRevenue.TabIndex = 1;
            lblTodayRevenue.Text = "0";
            // 
            // pnlCardTodayOrders
            // 
            pnlCardTodayOrders.Controls.Add(lblTodayOrdersTitle);
            pnlCardTodayOrders.Controls.Add(lblTodayOrders);
            pnlCardTodayOrders.BackColor = Color.FromArgb(37, 99, 235);
            pnlCardTodayOrders.Location = new Point(255, 3);
            pnlCardTodayOrders.Margin = new Padding(0, 0, 12, 12);
            pnlCardTodayOrders.Name = "pnlCardTodayOrders";
            pnlCardTodayOrders.Size = new Size(240, 100);
            pnlCardTodayOrders.TabIndex = 1;
            // 
            // lblTodayOrdersTitle
            // 
            lblTodayOrdersTitle.ForeColor = Color.White;
            lblTodayOrdersTitle.Location = new Point(14, 12);
            lblTodayOrdersTitle.Name = "lblTodayOrdersTitle";
            lblTodayOrdersTitle.Size = new Size(216, 22);
            lblTodayOrdersTitle.TabIndex = 0;
            lblTodayOrdersTitle.Text = "Hóa đơn đã xuất hôm nay";
            // 
            // lblTodayOrders
            // 
            lblTodayOrders.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTodayOrders.ForeColor = Color.White;
            lblTodayOrders.Location = new Point(12, 42);
            lblTodayOrders.Name = "lblTodayOrders";
            lblTodayOrders.Size = new Size(220, 40);
            lblTodayOrders.TabIndex = 1;
            lblTodayOrders.Text = "0";
            // 
            // pnlCardNewCustomers
            // 
            pnlCardNewCustomers.Controls.Add(lblNewCustomersTitle);
            pnlCardNewCustomers.Controls.Add(lblNewCustomers);
            pnlCardNewCustomers.BackColor = Color.FromArgb(245, 158, 11);
            pnlCardNewCustomers.Location = new Point(507, 3);
            pnlCardNewCustomers.Margin = new Padding(0, 0, 12, 12);
            pnlCardNewCustomers.Name = "pnlCardNewCustomers";
            pnlCardNewCustomers.Size = new Size(240, 100);
            pnlCardNewCustomers.TabIndex = 2;
            // 
            // lblNewCustomersTitle
            // 
            lblNewCustomersTitle.ForeColor = Color.White;
            lblNewCustomersTitle.Location = new Point(14, 12);
            lblNewCustomersTitle.Name = "lblNewCustomersTitle";
            lblNewCustomersTitle.Size = new Size(216, 22);
            lblNewCustomersTitle.TabIndex = 0;
            lblNewCustomersTitle.Text = "Khách hàng mới (tháng này)";
            // 
            // lblNewCustomers
            // 
            lblNewCustomers.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblNewCustomers.ForeColor = Color.White;
            lblNewCustomers.Location = new Point(12, 42);
            lblNewCustomers.Name = "lblNewCustomers";
            lblNewCustomers.Size = new Size(220, 40);
            lblNewCustomers.TabIndex = 1;
            lblNewCustomers.Text = "0";
            // 
            // pnlCardLowStock
            // 
            pnlCardLowStock.Controls.Add(lblLowStockTitle);
            pnlCardLowStock.Controls.Add(lblLowStock);
            pnlCardLowStock.BackColor = Color.FromArgb(220, 38, 38);
            pnlCardLowStock.Location = new Point(759, 3);
            pnlCardLowStock.Margin = new Padding(0, 0, 12, 12);
            pnlCardLowStock.Name = "pnlCardLowStock";
            pnlCardLowStock.Size = new Size(240, 100);
            pnlCardLowStock.TabIndex = 3;
            // 
            // lblLowStockTitle
            // 
            lblLowStockTitle.ForeColor = Color.White;
            lblLowStockTitle.Location = new Point(14, 12);
            lblLowStockTitle.Name = "lblLowStockTitle";
            lblLowStockTitle.Size = new Size(216, 22);
            lblLowStockTitle.TabIndex = 0;
            lblLowStockTitle.Text = "Mặt hàng sắp hết (tồn ≤ 10)";
            // 
            // lblLowStock
            // 
            lblLowStock.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLowStock.ForeColor = Color.White;
            lblLowStock.Location = new Point(12, 42);
            lblLowStock.Name = "lblLowStock";
            lblLowStock.Size = new Size(220, 40);
            lblLowStock.TabIndex = 1;
            lblLowStock.Text = "0";
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
            Controls.Add(chartRevenue);
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
            ((System.ComponentModel.ISupportInitialize)chartRevenue).EndInit();
            flpCards.ResumeLayout(false);
            flpCards.PerformLayout();
            pnlCardTodayRevenue.ResumeLayout(false);
            pnlCardTodayRevenue.PerformLayout();
            pnlCardTodayOrders.ResumeLayout(false);
            pnlCardTodayOrders.PerformLayout();
            pnlCardNewCustomers.ResumeLayout(false);
            pnlCardNewCustomers.PerformLayout();
            pnlCardLowStock.ResumeLayout(false);
            pnlCardLowStock.PerformLayout();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private FlowLayoutPanel flpCards;
        private Panel pnlCardTodayRevenue;
        private Label lblTodayRevenueTitle;
        private Label lblTodayRevenue;
        private Panel pnlCardTodayOrders;
        private Label lblTodayOrdersTitle;
        private Label lblTodayOrders;
        private Panel pnlCardNewCustomers;
        private Label lblNewCustomersTitle;
        private Label lblNewCustomers;
        private Panel pnlCardLowStock;
        private Label lblLowStockTitle;
        private Label lblLowStock;
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
