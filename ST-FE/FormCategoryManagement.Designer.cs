using System.ComponentModel;

namespace ST_FE
{
    partial class FormCategoryManagement
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
            dgvCategories = new DataGridView();
            btnSearch = new Button();
            btnadd = new Button();
            btnupdate = new Button();
            btndelete = new Button();
            txtKeyword = new TextBox();
            txtId = new TextBox();
            txtCategoryName = new TextBox();
            txtDescription = new TextBox();
            btnreload = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox3 = new GroupBox();
            ((ISupportInitialize)dgvCategories).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCategories
            // 
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(11, 16);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.Size = new Size(835, 492);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(624, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(111, 23);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tim kiem";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(882, 417);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(75, 39);
            btnadd.TabIndex = 2;
            btnadd.Text = "Them";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupdate
            // 
            btnupdate.Location = new Point(985, 417);
            btnupdate.Name = "btnupdate";
            btnupdate.Size = new Size(89, 38);
            btnupdate.TabIndex = 3;
            btnupdate.Text = "Cap nhat";
            btnupdate.UseVisualStyleBackColor = true;
            btnupdate.Click += btnupdate_Click;
            // 
            // btndelete
            // 
            btndelete.Location = new Point(1104, 416);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(81, 40);
            btndelete.TabIndex = 4;
            btndelete.Text = "Xoa";
            btndelete.UseVisualStyleBackColor = true;
            btndelete.Click += btndelete_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(13, 20);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(605, 23);
            txtKeyword.TabIndex = 5;
            // 
            // txtId
            // 
            txtId.Location = new Point(12, 49);
            txtId.Name = "txtId";
            txtId.Size = new Size(279, 23);
            txtId.TabIndex = 6;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(11, 116);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(280, 23);
            txtCategoryName.TabIndex = 7;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 185);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(281, 23);
            txtDescription.TabIndex = 8;
            // 
            // btnreload
            // 
            btnreload.Location = new Point(741, 19);
            btnreload.Name = "btnreload";
            btnreload.Size = new Size(102, 23);
            btnreload.TabIndex = 9;
            btnreload.Text = "Tai lai";
            btnreload.UseVisualStyleBackColor = true;
            btnreload.Click += btnreload_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtKeyword);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(btnreload);
            groupBox1.Location = new Point(12, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(849, 56);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tim Kiem";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(txtDescription);
            groupBox2.Controls.Add(txtCategoryName);
            groupBox2.Controls.Add(txtId);
            groupBox2.Location = new Point(876, 147);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(306, 244);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thong tin nhom hang";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 167);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 11;
            label3.Text = "Mo ta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 98);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 10;
            label2.Text = "Ten Nhom hang";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 31);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 9;
            label1.Text = "ID";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvCategories);
            groupBox3.Location = new Point(9, 107);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(852, 514);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Danh sach nhom hang";
            // 
            // FormCategoryManagement
            // 
            ClientSize = new Size(1207, 627);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btndelete);
            Controls.Add(btnupdate);
            Controls.Add(btnadd);
            Name = "FormCategoryManagement";
            Load += FormCategoryManagement_Load;
            ((ISupportInitialize)dgvCategories).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);

        }
        private DataGridView dgvCategories;
        private TextBox txtKeyword;
        private TextBox txtId;
        private TextBox txtCategoryName;
        private TextBox txtDescription;
        private Button btnSearch;
        private Button btnadd;
        private Button btnupdate;
        private Button btndelete;
        private Button btnreload;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label3;
        private Label label2;
        private GroupBox groupBox3;
    }
}
