namespace ST_FE.Forms
{
    partial class FormTokenInfo
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
            lblRaw = new Label();
            txtRaw = new TextBox();
            lblDecoded = new Label();
            txtDecoded = new TextBox();
            pnlBottom = new Panel();
            lblRefreshInfo = new Label();
            btnRefresh = new Button();
            btnClose = new Button();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblRaw
            // 
            lblRaw.Dock = DockStyle.Top;
            lblRaw.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRaw.Location = new Point(10, 10);
            lblRaw.Name = "lblRaw";
            lblRaw.Size = new Size(680, 24);
            lblRaw.TabIndex = 0;
            lblRaw.Text = "Access token (Header.Payload.Signature)";
            // 
            // txtRaw
            // 
            txtRaw.BackColor = Color.White;
            txtRaw.Dock = DockStyle.Top;
            txtRaw.Font = new Font("Consolas", 9F);
            txtRaw.Location = new Point(10, 34);
            txtRaw.Multiline = true;
            txtRaw.Name = "txtRaw";
            txtRaw.ReadOnly = true;
            txtRaw.ScrollBars = ScrollBars.Vertical;
            txtRaw.Size = new Size(680, 100);
            txtRaw.TabIndex = 1;
            // 
            // lblDecoded
            // 
            lblDecoded.Dock = DockStyle.Top;
            lblDecoded.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDecoded.Location = new Point(10, 134);
            lblDecoded.Name = "lblDecoded";
            lblDecoded.Padding = new Padding(0, 8, 0, 0);
            lblDecoded.Size = new Size(680, 32);
            lblDecoded.TabIndex = 2;
            lblDecoded.Text = "Nội dung giải mã (Base64Url) — ai cũng đọc được, nhưng không thể sửa vì có chữ ký HMAC-SHA256";
            // 
            // txtDecoded
            // 
            txtDecoded.BackColor = Color.White;
            txtDecoded.Dock = DockStyle.Fill;
            txtDecoded.Font = new Font("Consolas", 10F);
            txtDecoded.Location = new Point(10, 166);
            txtDecoded.Multiline = true;
            txtDecoded.Name = "txtDecoded";
            txtDecoded.ReadOnly = true;
            txtDecoded.ScrollBars = ScrollBars.Both;
            txtDecoded.Size = new Size(680, 314);
            txtDecoded.TabIndex = 3;
            txtDecoded.WordWrap = false;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(lblRefreshInfo);
            pnlBottom.Controls.Add(btnRefresh);
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(10, 480);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(680, 60);
            pnlBottom.TabIndex = 4;
            // 
            // lblRefreshInfo
            // 
            lblRefreshInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblRefreshInfo.ForeColor = Color.FromArgb(75, 85, 99);
            lblRefreshInfo.Location = new Point(0, 10);
            lblRefreshInfo.Name = "lblRefreshInfo";
            lblRefreshInfo.Size = new Size(380, 42);
            lblRefreshInfo.TabIndex = 0;
            lblRefreshInfo.Text = "Refresh token:";
            lblRefreshInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(390, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(180, 38);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Làm mới token ngay";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(580, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 2;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // FormTokenInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(700, 550);
            Controls.Add(txtDecoded);
            Controls.Add(lblDecoded);
            Controls.Add(txtRaw);
            Controls.Add(lblRaw);
            Controls.Add(pnlBottom);
            Font = new Font("Segoe UI", 10F);
            MinimizeBox = false;
            Name = "FormTokenInfo";
            Padding = new Padding(10);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Phiên đăng nhập (JWT)";
            Load += FormTokenInfo_Load;
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRaw;
        private TextBox txtRaw;
        private Label lblDecoded;
        private TextBox txtDecoded;
        private Panel pnlBottom;
        private Label lblRefreshInfo;
        private Button btnRefresh;
        private Button btnClose;
    }
}
