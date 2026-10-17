namespace ST_FE
{
    partial class FormQuickReport
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
            lblDate = new Label();
            dtpReportDate = new DateTimePicker();
            btnRunReport = new Button();
            panelOrders = new Panel();
            lblOrdersCaption = new Label();
            lblTotalOrders = new Label();
            panelRevenue = new Panel();
            lblRevenueCaption = new Label();
            lblTotalRevenue = new Label();
            panelBestSeller = new Panel();
            lblBestSellerCaption = new Label();
            lblBestSeller = new Label();
            panelOrders.SuspendLayout();
            panelRevenue.SuspendLayout();
            panelBestSeller.SuspendLayout();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDate.Location = new Point(20, 28);
            lblDate.Name = "lblDate";
            lblDate.Text = "Ngày báo cáo:";
            // 
            // dtpReportDate
            // 
            dtpReportDate.CustomFormat = "dd/MM/yyyy";
            dtpReportDate.Font = new Font("Segoe UI", 11F);
            dtpReportDate.Format = DateTimePickerFormat.Custom;
            dtpReportDate.Location = new Point(140, 24);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(160, 27);
            dtpReportDate.TabIndex = 0;
            // 
            // btnRunReport
            // 
            btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(320, 20);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(180, 36);
            btnRunReport.TabIndex = 1;
            btnRunReport.Text = "📊 Xem doanh thu ca";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;
            // 
            // panelOrders
            // 
            panelOrders.BackColor = Color.FromArgb(13, 110, 253);
            panelOrders.Controls.Add(lblTotalOrders);
            panelOrders.Controls.Add(lblOrdersCaption);
            panelOrders.Location = new Point(20, 90);
            panelOrders.Name = "panelOrders";
            panelOrders.Size = new Size(310, 150);
            panelOrders.TabIndex = 3;
            // 
            // lblOrdersCaption
            // 
            lblOrdersCaption.Dock = DockStyle.Top;
            lblOrdersCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOrdersCaption.ForeColor = Color.White;
            lblOrdersCaption.Name = "lblOrdersCaption";
            lblOrdersCaption.Padding = new Padding(10, 10, 0, 0);
            lblOrdersCaption.Size = new Size(310, 45);
            lblOrdersCaption.Text = "Tổng số hóa đơn";
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Text = "0";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRevenue
            // 
            panelRevenue.BackColor = Color.FromArgb(25, 135, 84);
            panelRevenue.Controls.Add(lblTotalRevenue);
            panelRevenue.Controls.Add(lblRevenueCaption);
            panelRevenue.Location = new Point(350, 90);
            panelRevenue.Name = "panelRevenue";
            panelRevenue.Size = new Size(310, 150);
            panelRevenue.TabIndex = 4;
            // 
            // lblRevenueCaption
            // 
            lblRevenueCaption.Dock = DockStyle.Top;
            lblRevenueCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRevenueCaption.ForeColor = Color.White;
            lblRevenueCaption.Name = "lblRevenueCaption";
            lblRevenueCaption.Padding = new Padding(10, 10, 0, 0);
            lblRevenueCaption.Size = new Size(310, 45);
            lblRevenueCaption.Text = "Tổng doanh thu";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Text = "0 đ";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelBestSeller
            // 
            panelBestSeller.BackColor = Color.FromArgb(255, 140, 0);
            panelBestSeller.Controls.Add(lblBestSeller);
            panelBestSeller.Controls.Add(lblBestSellerCaption);
            panelBestSeller.Location = new Point(680, 90);
            panelBestSeller.Name = "panelBestSeller";
            panelBestSeller.Size = new Size(310, 150);
            panelBestSeller.TabIndex = 5;
            // 
            // lblBestSellerCaption
            // 
            lblBestSellerCaption.Dock = DockStyle.Top;
            lblBestSellerCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBestSellerCaption.ForeColor = Color.White;
            lblBestSellerCaption.Name = "lblBestSellerCaption";
            lblBestSellerCaption.Padding = new Padding(10, 10, 0, 0);
            lblBestSellerCaption.Size = new Size(310, 45);
            lblBestSellerCaption.Text = "Mặt hàng bán chạy nhất";
            // 
            // lblBestSeller
            // 
            lblBestSeller.Dock = DockStyle.Fill;
            lblBestSeller.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.White;
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Text = "—";
            lblBestSeller.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormQuickReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1034, 621);
            Controls.Add(lblDate);
            Controls.Add(dtpReportDate);
            Controls.Add(btnRunReport);
            Controls.Add(panelOrders);
            Controls.Add(panelRevenue);
            Controls.Add(panelBestSeller);
            Name = "FormQuickReport";
            Text = "Báo cáo Doanh thu Nhanh";
            Load += FormQuickReport_Load;
            panelOrders.ResumeLayout(false);
            panelRevenue.ResumeLayout(false);
            panelBestSeller.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;
        private Panel panelOrders;
        private Label lblOrdersCaption;
        private Label lblTotalOrders;
        private Panel panelRevenue;
        private Label lblRevenueCaption;
        private Label lblTotalRevenue;
        private Panel panelBestSeller;
        private Label lblBestSellerCaption;
        private Label lblBestSeller;
    }
}
