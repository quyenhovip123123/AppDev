namespace ST_FE
{
    partial class Login
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
            groupBox1 = new GroupBox();
            btnLogin = new Button();
            label2 = new Label();
            label1 = new Label();
            txtPass = new TextBox();
            txtUser = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLogin);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtPass);
            groupBox1.Controls.Add(txtUser);
            groupBox1.Location = new Point(264, 70);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(302, 305);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Đăng nhập";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(13, 224);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(283, 35);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 148);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Mật khẩu";
            label2.Font = new Font("Nunito",10F);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 69);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 2;
            label1.Text = "Tài khoản";
            label1.Font = new Font("Nunito", 10F);
            // 
            // txtPass
            // 
            txtPass.Location = new Point(138, 145);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(158, 23);
            txtPass.TabIndex = 1;
            txtPass.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(138, 66);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Ví dụ: admin";
            txtUser.Size = new Size(158, 23);
            txtUser.TabIndex = 0;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Name = "Login";
            Text = "Login";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private GroupBox groupBox1;
        private Button btnLogin;
        private Label label2;
        private Label label1;
        private TextBox txtPass;
        private TextBox txtUser;
    }
}