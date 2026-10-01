namespace ST_FE.Forms
{
    partial class FormOrderDetail
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
            dgvDetails = new DataGridView();
            pnlHeader = new Panel();
            lblOrderCode = new Label();
            lblInfoLeft = new Label();
            lblInfoRight = new Label();
            pnlFooter = new Panel();
            lblTotals = new Label();
            btnPrint = new Button();
            btnClose = new Button();
            lblNote = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            pnlHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDetails
            // 
            dgvDetails.Dock = DockStyle.Fill;
            dgvDetails.Location = new Point(12, 120);
            dgvDetails.Name = "dgvDetails";
            dgvDetails.Size = new Size(636, 250);
            dgvDetails.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblOrderCode);
            pnlHeader.Controls.Add(lblInfoLeft);
            pnlHeader.Controls.Add(lblInfoRight);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(12, 12);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(636, 108);
            pnlHeader.TabIndex = 1;
            // 
            // lblOrderCode
            // 
            lblOrderCode.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblOrderCode.Location = new Point(0, 0);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.Size = new Size(636, 30);
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "HÓA ĐƠN";
            // 
            // lblInfoLeft
            // 
            lblInfoLeft.Location = new Point(0, 34);
            lblInfoLeft.Name = "lblInfoLeft";
            lblInfoLeft.Size = new Size(320, 70);
            lblInfoLeft.TabIndex = 1;
            lblInfoLeft.Text = "";
            // 
            // lblInfoRight
            // 
            lblInfoRight.Location = new Point(330, 34);
            lblInfoRight.Name = "lblInfoRight";
            lblInfoRight.Size = new Size(306, 70);
            lblInfoRight.TabIndex = 2;
            lblInfoRight.Text = "";
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(lblTotals);
            pnlFooter.Controls.Add(btnPrint);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(lblNote);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(12, 370);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(636, 150);
            pnlFooter.TabIndex = 2;
            // 
            // lblTotals
            // 
            lblTotals.Font = new Font("Segoe UI", 10.5F);
            lblTotals.Location = new Point(276, 6);
            lblTotals.Name = "lblTotals";
            lblTotals.Size = new Size(360, 96);
            lblTotals.TabIndex = 0;
            lblTotals.Text = "";
            lblTotals.TextAlign = ContentAlignment.TopRight;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(386, 106);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(140, 38);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "🖨 In hóa đơn";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnClose
            // 
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(536, 106);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 2;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            lblNote.ForeColor = Color.FromArgb(107, 114, 128);
            lblNote.Location = new Point(0, 6);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(270, 96);
            lblNote.TabIndex = 3;
            lblNote.Text = "";
            // 
            // FormOrderDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnClose;
            ClientSize = new Size(660, 532);
            Controls.Add(dgvDetails);
            Controls.Add(pnlHeader);
            Controls.Add(pnlFooter);
            Font = new Font("Segoe UI", 10F);
            MinimizeBox = false;
            Name = "FormOrderDetail";
            Padding = new Padding(12);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết hóa đơn";
            Load += FormOrderDetail_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDetails;
        private Panel pnlHeader;
        private Label lblOrderCode;
        private Label lblInfoLeft;
        private Label lblInfoRight;
        private Panel pnlFooter;
        private Label lblTotals;
        private Button btnPrint;
        private Button btnClose;
        private Label lblNote;
    }
}
