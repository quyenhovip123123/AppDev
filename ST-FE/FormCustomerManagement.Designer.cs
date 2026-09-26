using System.ComponentModel;

namespace ST_FE
{
    partial class FormCustomerManagement
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        #endregion
        private void InitializeComponent()
        {
            dgvCustomer = new DataGridView();
            btn_search = new Button();
            btn_add = new Button();
            btn_update = new Button();
            btn_delete = new Button();
            txtSearch = new TextBox();
            txtCustomerId = new TextBox();
            txtCustomerName = new TextBox();
            txt_Description = new TextBox();
            btn_reload = new Button();
            gb1 = new GroupBox();
            gb2 = new GroupBox();
            l3 = new Label();
            l2 = new Label();
            l1 = new Label();
            gb3 = new GroupBox();
            ((ISupportInitialize)dgvCustomer).BeginInit();
            gb1.SuspendLayout();
            gb2.SuspendLayout();
            gb3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomer
            // 
            dgvCustomer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomer.Location = new Point(11, 16);
            dgvCustomer.Name = "dgvCustomer";
            dgvCustomer.Size = new Size(835, 492);
            dgvCustomer.TabIndex = 0;
            dgvCustomer.CellClick += dgvCustomer_CellClick;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(624, 20);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(111, 23);
            btn_search.TabIndex = 1;
            btn_search.Text = "Tim kiem";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // btn_add
            // 
            btn_add.Location = new Point(882, 417);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(75, 39);
            btn_add.TabIndex = 2;
            btn_add.Text = "Them";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // btn_update
            // 
            btn_update.Location = new Point(985, 417);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(89, 38);
            btn_update.TabIndex = 3;
            btn_update.Text = "Cap nhat";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(1104, 416);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(81, 40);
            btn_delete.TabIndex = 4;
            btn_delete.Text = "Xoa";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(13, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(605, 23);
            txtSearch.TabIndex = 5;
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(12, 49);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.Size = new Size(279, 23);
            txtCustomerId.TabIndex = 6;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(11, 116);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(280, 23);
            txtCustomerName.TabIndex = 7;
            // 
            // txt_Description
            // 
            txt_Description.Location = new Point(12, 185);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(281, 23);
            txt_Description.TabIndex = 8;
            // 
            // btn_reload
            // 
            btn_reload.Location = new Point(741, 19);
            btn_reload.Name = "btn_reload";
            btn_reload.Size = new Size(102, 23);
            btn_reload.TabIndex = 9;
            btn_reload.Text = "Tai lai";
            btn_reload.UseVisualStyleBackColor = true;
            btn_reload.Click += btn_reload_Click;
            // 
            // gb1
            // 
            gb1.Controls.Add(txtSearch);
            gb1.Controls.Add(btn_search);
            gb1.Controls.Add(btn_reload);
            gb1.Location = new Point(12, 14);
            gb1.Name = "gb1";
            gb1.Size = new Size(849, 56);
            gb1.TabIndex = 10;
            gb1.TabStop = false;
            gb1.Text = "Tim Kiem";
            // 
            // gb2
            // 
            gb2.Controls.Add(l3);
            gb2.Controls.Add(l2);
            gb2.Controls.Add(l1);
            gb2.Controls.Add(txt_Description);
            gb2.Controls.Add(txtCustomerName);
            gb2.Controls.Add(txtCustomerId);
            gb2.Location = new Point(876, 147);
            gb2.Name = "gb2";
            gb2.Size = new Size(306, 244);
            gb2.TabIndex = 11;
            gb2.TabStop = false;
            gb2.Text = "Thong tin nhom hang";
            // 
            // l3
            // 
            l3.AutoSize = true;
            l3.Location = new Point(11, 167);
            l3.Name = "l3";
            l3.Size = new Size(38, 15);
            l3.TabIndex = 11;
            l3.Text = "Mo ta";
            // 
            // l2
            // 
            l2.AutoSize = true;
            l2.Location = new Point(12, 98);
            l2.Name = "l2";
            l2.Size = new Size(92, 15);
            l2.TabIndex = 10;
            l2.Text = "Ten Nhom hang";
            // 
            // l1
            // 
            l1.AutoSize = true;
            l1.Location = new Point(12, 31);
            l1.Name = "l1";
            l1.Size = new Size(18, 15);
            l1.TabIndex = 9;
            l1.Text = "ID";
            // 
            // gb3
            // 
            gb3.Controls.Add(dgvCustomer);
            gb3.Location = new Point(9, 107);
            gb3.Name = "gb3";
            gb3.Size = new Size(852, 514);
            gb3.TabIndex = 12;
            gb3.TabStop = false;
            gb3.Text = "Danh sach nhom hang";
            // 
            // FormCategoryManagement
            // 
            ClientSize = new Size(1207, 627);
            Controls.Add(gb3);
            Controls.Add(gb2);
            Controls.Add(gb1);
            Controls.Add(btn_delete);
            Controls.Add(btn_update);
            Controls.Add(btn_add);
            Name = "FormCustomerManagement";
            Load += FormCustomerManagement_Load;
            ((ISupportInitialize)dgvCustomer).EndInit();
            gb1.ResumeLayout(false);
            gb1.PerformLayout();
            gb2.ResumeLayout(false);
            gb2.PerformLayout();
            gb3.ResumeLayout(false);
            ResumeLayout(false);

        }
        private DataGridView dgvCustomer;
        private TextBox txtSearch;
        private TextBox txtCustomerId;
        private TextBox txtCustomerName;
        private TextBox txt_Description;
        private Button btn_search;
        private Button btn_add;
        private Button btn_update;
        private Button btn_delete;
        private Button btn_reload;
        private GroupBox gb1;
        private GroupBox gb2;
        private Label l1;
        private Label l3;
        private Label l2;
        private GroupBox gb3;
    }
}
