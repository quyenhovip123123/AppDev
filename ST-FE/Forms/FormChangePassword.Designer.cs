namespace ST_FE.Forms
{
    partial class FormChangePassword
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
            lblCurrent = new Label();
            txtCurrent = new TextBox();
            lblNew = new Label();
            txtNew = new TextBox();
            lblConfirm = new Label();
            txtConfirm = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblNote = new Label();
            SuspendLayout();
            // 
            // lblCurrent
            // 
            lblCurrent.AutoSize = true;
            lblCurrent.Location = new Point(30, 20);
            lblCurrent.Name = "lblCurrent";
            lblCurrent.Size = new Size(125, 19);
            lblCurrent.TabIndex = 0;
            lblCurrent.Text = "Mật khẩu hiện tại";
            // 
            // txtCurrent
            // 
            txtCurrent.Location = new Point(30, 42);
            txtCurrent.Name = "txtCurrent";
            txtCurrent.Size = new Size(320, 25);
            txtCurrent.TabIndex = 1;
            txtCurrent.UseSystemPasswordChar = true;
            // 
            // lblNew
            // 
            lblNew.AutoSize = true;
            lblNew.Location = new Point(30, 78);
            lblNew.Name = "lblNew";
            lblNew.Size = new Size(170, 19);
            lblNew.TabIndex = 2;
            lblNew.Text = "Mật khẩu mới (≥ 6 ký tự)";
            // 
            // txtNew
            // 
            txtNew.Location = new Point(30, 100);
            txtNew.Name = "txtNew";
            txtNew.Size = new Size(320, 25);
            txtNew.TabIndex = 3;
            txtNew.UseSystemPasswordChar = true;
            // 
            // lblConfirm
            // 
            lblConfirm.AutoSize = true;
            lblConfirm.Location = new Point(30, 136);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(152, 19);
            lblConfirm.TabIndex = 4;
            lblConfirm.Text = "Nhập lại mật khẩu mới";
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(30, 158);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.Size = new Size(320, 25);
            txtConfirm.TabIndex = 5;
            txtConfirm.UseSystemPasswordChar = true;
            // 
            // lblNote
            // 
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(30, 192);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(320, 40);
            lblNote.TabIndex = 6;
            lblNote.Text = "Sau khi đổi, mọi phiên đăng nhập khác của tài khoản sẽ bị đăng xuất.";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(130, 240);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 36);
            btnSave.TabIndex = 7;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(260, 240);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 36);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormChangePassword
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(380, 295);
            Controls.Add(lblNote);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtConfirm);
            Controls.Add(lblConfirm);
            Controls.Add(txtNew);
            Controls.Add(lblNew);
            Controls.Add(txtCurrent);
            Controls.Add(lblCurrent);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormChangePassword";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCurrent;
        private TextBox txtCurrent;
        private Label lblNew;
        private TextBox txtNew;
        private Label lblConfirm;
        private TextBox txtConfirm;
        private Label lblNote;
        private Button btnSave;
        private Button btnCancel;
    }
}
