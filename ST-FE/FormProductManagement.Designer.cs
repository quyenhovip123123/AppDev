namespace ST_FE
{
    partial class FormProductManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSearchBarcode;
        private System.Windows.Forms.TextBox txtSearchBarcode;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvProducts;

        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;

        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;

        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;

        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown nudStock;

        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "FormProductManagerment";
        }

        #endregion
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSearchBarcode = new System.Windows.Forms.Label();
            this.txtSearchBarcode = new System.Windows.Forms.TextBox();
            this.lblFilterCategory = new System.Windows.Forms.Label();
            this.cboFilterCategory = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();

            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvProducts = new System.Windows.Forms.DataGridView();

            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();

            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();

            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();

            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();

            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();

            this.lblStock = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();

            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();

            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();

            this.pnlTop.SuspendLayout();
            this.pnlDetail.SuspendLayout();

            this.SuspendLayout();

            // =========================
            // FORM
            // =========================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1200, 700);

            this.MinimumSize =
                new System.Drawing.Size(1000, 600);

            this.Name =
                "FormProductManagement";

            this.Text =
                "Quản lý sản phẩm & kho hàng";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Load +=
                new System.EventHandler(
                    this.FormProductManagement_Load);

            // =========================
            // TOP PANEL
            // =========================

            this.pnlTop.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.pnlTop.Height = 70;

            this.pnlTop.BackColor =
                System.Drawing.Color.WhiteSmoke;

            // Search barcode label

            this.lblSearchBarcode.AutoSize = true;

            this.lblSearchBarcode.Location =
                new System.Drawing.Point(20, 25);

            this.lblSearchBarcode.Text =
                "Mã vạch:";

            this.lblSearchBarcode.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlTop.Controls.Add(
                this.lblSearchBarcode);

            // Search barcode textbox

            this.txtSearchBarcode.Location =
                new System.Drawing.Point(90, 20);

            this.txtSearchBarcode.Size =
                new System.Drawing.Size(220, 27);

            this.txtSearchBarcode.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlTop.Controls.Add(
                this.txtSearchBarcode);

            // Filter label

            this.lblFilterCategory.AutoSize = true;

            this.lblFilterCategory.Location =
                new System.Drawing.Point(340, 25);

            this.lblFilterCategory.Text =
                "Nhóm hàng:";

            this.lblFilterCategory.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlTop.Controls.Add(
                this.lblFilterCategory);

            // Filter combo

            this.cboFilterCategory.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboFilterCategory.Location =
                new System.Drawing.Point(430, 20);

            this.cboFilterCategory.Size =
                new System.Drawing.Size(220, 28);

            this.cboFilterCategory.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlTop.Controls.Add(
                this.cboFilterCategory);

            // Search button

            this.btnSearch.Location =
                new System.Drawing.Point(670, 18);

            this.btnSearch.Size =
                new System.Drawing.Size(110, 35);

            this.btnSearch.Text =
                "🔍 Tìm kiếm";

            this.btnSearch.BackColor =
                System.Drawing.Color.FromArgb(
                    0, 123, 255);

            this.btnSearch.ForeColor =
                System.Drawing.Color.White;

            this.btnSearch.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSearch.Click +=
                new System.EventHandler(
                    this.btnSearch_Click);

            this.pnlTop.Controls.Add(
                this.btnSearch);

            // =========================
            // SPLIT CONTAINER
            // =========================

            this.splitContainer.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.splitContainer.Orientation =
                System.Windows.Forms.Orientation.Vertical;

            // 68% trái / 32% phải
            this.splitContainer.SplitterDistance = 800;

            this.splitContainer.IsSplitterFixed = false;

            // =========================
            // DATAGRIDVIEW
            // =========================

            this.dgvProducts.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;

            this.dgvProducts.AllowUserToResizeRows = false;

            this.dgvProducts.AutoGenerateColumns = true;

            this.dgvProducts.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvProducts.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.dgvProducts.ColumnHeadersHeight = 40;

            this.dgvProducts.MultiSelect = false;

            this.dgvProducts.ReadOnly = true;

            this.dgvProducts.RowHeadersVisible = false;

            this.dgvProducts.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvProducts.CellClick +=
                new System.Windows.Forms.DataGridViewCellEventHandler(
                    this.dgvProducts_CellClick);

            this.splitContainer.Panel1.Controls.Add(
                this.dgvProducts);

            // =========================
            // DETAIL PANEL
            // =========================

            this.pnlDetail.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.pnlDetail.BackColor =
                System.Drawing.Color.White;

            this.splitContainer.Panel2.Controls.Add(
                this.pnlDetail);

            // Title

            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    16F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    33, 37, 41);

            this.lblTitle.Location =
                new System.Drawing.Point(25, 20);

            this.lblTitle.Text =
                "Chi tiết sản phẩm";

            this.pnlDetail.Controls.Add(
                this.lblTitle);

            // =========================
            // ID
            // =========================

            this.lblId.AutoSize = true;

            this.lblId.Location =
                new System.Drawing.Point(25, 75);

            this.lblId.Text =
                "Product ID:";

            this.lblId.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblId);

            this.txtId.Location =
                new System.Drawing.Point(25, 100);

            this.txtId.Size =
                new System.Drawing.Size(300, 27);

            this.txtId.ReadOnly = true;

            this.txtId.BackColor =
                System.Drawing.Color.Gainsboro;

            this.pnlDetail.Controls.Add(
                this.txtId);

            // =========================
            // BARCODE
            // =========================

            this.lblBarcode.AutoSize = true;

            this.lblBarcode.Location =
                new System.Drawing.Point(25, 140);

            this.lblBarcode.Text =
                "Mã vạch:";

            this.lblBarcode.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblBarcode);

            this.txtBarcode.Location =
                new System.Drawing.Point(25, 165);

            this.txtBarcode.Size =
                new System.Drawing.Size(300, 27);

            this.pnlDetail.Controls.Add(
                this.txtBarcode);

            // =========================
            // PRODUCT NAME
            // =========================

            this.lblProductName.AutoSize = true;

            this.lblProductName.Location =
                new System.Drawing.Point(25, 205);

            this.lblProductName.Text =
                "Tên sản phẩm:";

            this.lblProductName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblProductName);

            this.txtProductName.Location =
                new System.Drawing.Point(25, 230);

            this.txtProductName.Size =
                new System.Drawing.Size(300, 27);

            this.pnlDetail.Controls.Add(
                this.txtProductName);

            // =========================
            // PRICE
            // =========================

            this.lblPrice.AutoSize = true;

            this.lblPrice.Location =
                new System.Drawing.Point(25, 270);

            this.lblPrice.Text =
                "Đơn giá:";

            this.lblPrice.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblPrice);

            this.nudPrice.Location =
                new System.Drawing.Point(25, 295);

            this.nudPrice.Size =
                new System.Drawing.Size(300, 27);

            this.nudPrice.Maximum =
                1000000000;

            this.nudPrice.Minimum =
                0;

            this.nudPrice.DecimalPlaces =
                0;

            this.nudPrice.ThousandsSeparator =
                true;

            this.pnlDetail.Controls.Add(
                this.nudPrice);

            // =========================
            // STOCK
            // =========================

            this.lblStock.AutoSize = true;

            this.lblStock.Location =
                new System.Drawing.Point(25, 335);

            this.lblStock.Text =
                "Tồn kho:";

            this.lblStock.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblStock);

            this.nudStock.Location =
                new System.Drawing.Point(25, 360);

            this.nudStock.Size =
                new System.Drawing.Size(300, 27);

            this.nudStock.Maximum =
                1000000;

            this.nudStock.Minimum =
                0;

            this.pnlDetail.Controls.Add(
                this.nudStock);

            // =========================
            // CATEGORY
            // =========================

            this.lblCategory.AutoSize = true;

            this.lblCategory.Location =
                new System.Drawing.Point(25, 400);

            this.lblCategory.Text =
                "Nhóm hàng:";

            this.lblCategory.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.pnlDetail.Controls.Add(
                this.lblCategory);

            this.cboCategory.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboCategory.Location =
                new System.Drawing.Point(25, 425);

            this.cboCategory.Size =
                new System.Drawing.Size(300, 28);

            this.pnlDetail.Controls.Add(
                this.cboCategory);

            // =========================
            // BUTTON LOAD
            // =========================

            this.btnLoad.Location =
                new System.Drawing.Point(25, 480);

            this.btnLoad.Size =
                new System.Drawing.Size(140, 40);

            this.btnLoad.Text =
                "↻ Làm mới";

            this.btnLoad.BackColor =
                System.Drawing.Color.FromArgb(
                    108, 117, 125);

            this.btnLoad.ForeColor =
                System.Drawing.Color.White;

            this.btnLoad.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnLoad.Click +=
                new System.EventHandler(
                    this.btnLoad_Click);

            this.pnlDetail.Controls.Add(
                this.btnLoad);

            // =========================
            // BUTTON ADD
            // =========================

            this.btnAdd.Location =
                new System.Drawing.Point(180, 480);

            this.btnAdd.Size =
                new System.Drawing.Size(145, 40);

            this.btnAdd.Text =
                "＋ Thêm";

            this.btnAdd.BackColor =
                System.Drawing.Color.FromArgb(
                    40, 167, 69);

            this.btnAdd.ForeColor =
                System.Drawing.Color.White;

            this.btnAdd.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnAdd.Click +=
                new System.EventHandler(
                    this.btnAdd_Click);

            this.pnlDetail.Controls.Add(
                this.btnAdd);

            // =========================
            // BUTTON UPDATE
            // =========================

            this.btnUpdate.Location =
                new System.Drawing.Point(25, 535);

            this.btnUpdate.Size =
                new System.Drawing.Size(140, 40);

            this.btnUpdate.Text =
                "✎ Cập nhật";

            this.btnUpdate.BackColor =
                System.Drawing.Color.FromArgb(
                    255, 193, 7);

            this.btnUpdate.ForeColor =
                System.Drawing.Color.Black;

            this.btnUpdate.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnUpdate.Click +=
                new System.EventHandler(
                    this.btnUpdate_Click);

            this.pnlDetail.Controls.Add(
                this.btnUpdate);

            // =========================
            // BUTTON DELETE
            // =========================

            this.btnDelete.Location =
                new System.Drawing.Point(180, 535);

            this.btnDelete.Size =
                new System.Drawing.Size(145, 40);

            this.btnDelete.Text =
                "🗑 Xóa";

            this.btnDelete.BackColor =
                System.Drawing.Color.FromArgb(
                    220, 53, 69);

            this.btnDelete.ForeColor =
                System.Drawing.Color.White;

            this.btnDelete.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnDelete.Click +=
                new System.EventHandler(
                    this.btnDelete_Click);

            this.pnlDetail.Controls.Add(
                this.btnDelete);

            // =========================
            // ADD CONTROLS
            // =========================

            this.Controls.Add(
                this.splitContainer);

            this.Controls.Add(
                this.pnlTop);

            // =========================
            // CLEANUP
            // =========================

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();

            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvProducts)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudPrice)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudStock)).EndInit();

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.splitContainer)).EndInit();

            this.splitContainer.ResumeLayout(false);

            this.ResumeLayout(false);
        }
    }
}
}