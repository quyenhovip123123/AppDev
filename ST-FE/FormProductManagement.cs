using ST_FE;
using ST_FE.WinForms;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST_FE.WinForms
{
    public partial class FormProductManagement : Form
    {
        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
        }

        // =========================
        // LOAD CATEGORY
        // =========================
        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                var categories =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<CategoryDto>>("categories");

                if (categories == null)
                    return;

                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                // ComboBox lọc
                var filterCategories = new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        CategoryId = 0,
                        CategoryName = "Tất cả danh mục"
                    }
                };

                filterCategories.AddRange(categories);

                cboFilterCategory.DataSource = filterCategories;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi nạp danh mục: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // LOAD PRODUCTS
        // =========================
        private async Task LoadProductsAsync()
        {
            try
            {
                var products =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<ProductDto>>("products");

                if (products == null)
                {
                    dgvProducts.DataSource = null;
                    return;
                }

                dgvProducts.DataSource = products;

                ConfigureGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi nạp sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // CONFIG DATAGRIDVIEW
        // =========================
        private void ConfigureGrid()
        {
            if (dgvProducts.Columns.Count == 0)
                return;

            if (dgvProducts.Columns["ProductId"] != null)
                dgvProducts.Columns["ProductId"].HeaderText = "Product ID";

            if (dgvProducts.Columns["Barcode"] != null)
                dgvProducts.Columns["Barcode"].HeaderText = "Mã vạch";

            if (dgvProducts.Columns["ProductName"] != null)
                dgvProducts.Columns["ProductName"].HeaderText = "Tên sản phẩm";

            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Đơn giá";
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N0";
            }

            if (dgvProducts.Columns["StockQuantity"] != null)
                dgvProducts.Columns["StockQuantity"].HeaderText = "Tồn kho";

            if (dgvProducts.Columns["CategoryName"] != null)
                dgvProducts.Columns["CategoryName"].HeaderText = "Nhóm hàng";

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =========================
        // CLICK DATAGRIDVIEW
        // =========================
        private void dgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                var row = dgvProducts.Rows[e.RowIndex];

                txtId.Text =
                    row.Cells["ProductId"].Value?.ToString() ?? "";

                txtBarcode.Text =
                    row.Cells["Barcode"].Value?.ToString() ?? "";

                txtProductName.Text =
                    row.Cells["ProductName"].Value?.ToString() ?? "";

                if (row.Cells["Price"].Value != null)
                {
                    decimal price =
                        Convert.ToDecimal(row.Cells["Price"].Value);

                    if (price >= nudPrice.Minimum &&
                        price <= nudPrice.Maximum)
                    {
                        nudPrice.Value = price;
                    }
                }

                if (row.Cells["StockQuantity"].Value != null)
                {
                    int stock =
                        Convert.ToInt32(row.Cells["StockQuantity"].Value);

                    if (stock >= nudStock.Minimum &&
                        stock <= nudStock.Maximum)
                    {
                        nudStock.Value = stock;
                    }
                }

                // Nếu ProductDto có CategoryId
                if (row.Cells["CategoryId"] != null &&
                    row.Cells["CategoryId"].Value != null)
                {
                    cboCategory.SelectedValue =
                        Convert.ToInt32(row.Cells["CategoryId"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể lấy dữ liệu sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // ADD
        // =========================
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                var newProd = new
                {
                    Barcode = txtBarcode.Text.Trim(),
                    ProductName = txtProductName.Text.Trim(),
                    Price = nudPrice.Value,
                    StockQuantity = (int)nudStock.Value,
                    CategoryId = Convert.ToInt32(cboCategory.SelectedValue)
                };

                var res =
                    await ApiClientService.Client
                        .PostAsJsonAsync("products", newProd);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm mới sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    string error = await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm sản phẩm thất bại.\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // UPDATE
        // =========================
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần cập nhật.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateInput())
                return;

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Product ID không hợp lệ.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                var updateProd = new
                {
                    ProductId = id,
                    Barcode = txtBarcode.Text.Trim(),
                    ProductName = txtProductName.Text.Trim(),
                    Price = nudPrice.Value,
                    StockQuantity = (int)nudStock.Value,
                    CategoryId = Convert.ToInt32(cboCategory.SelectedValue)
                };

                var res =
                    await ApiClientService.Client
                        .PutAsJsonAsync(
                            $"products/{id}",
                            updateProd);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadProductsAsync();
                }
                else
                {
                    string error = await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật thất bại.\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // DELETE
        // =========================
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Product ID không hợp lệ.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm ID = {id}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var res =
                    await ApiClientService.Client
                        .DeleteAsync($"products/{id}");

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Đã xóa sản phẩm!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    string error = await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa sản phẩm thất bại.\n" + error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi xóa sản phẩm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // LOAD / REFRESH
        // =========================
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            ClearInputs();
            await LoadProductsAsync();
        }

        // =========================
        // SEARCH
        // =========================
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string barcode = txtSearchBarcode.Text.Trim();

                int categoryId = 0;

                if (cboFilterCategory.SelectedValue != null)
                {
                    int.TryParse(
                        cboFilterCategory.SelectedValue.ToString(),
                        out categoryId);
                }

                var products =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<ProductDto>>("products");

                if (products == null)
                    return;

                IEnumerable<ProductDto> result = products;

                // Lọc mã vạch
                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    result = System.Linq.Enumerable.Where(
                        result,
                        p => p.Barcode != null &&
                             p.Barcode.Contains(
                                 barcode,
                                 StringComparison.OrdinalIgnoreCase));
                }

                // Lọc danh mục
                if (categoryId > 0)
                {
                    result = System.Linq.Enumerable.Where(
                        result,
                        p => p.CategoryId == categoryId);
                }

                dgvProducts.DataSource =
                    new List<ProductDto>(
                        System.Linq.Enumerable.ToList(result));

                ConfigureGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // VALIDATE
        // =========================
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã vạch.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtBarcode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtProductName.Focus();
                return false;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn danh mục.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboCategory.Focus();
                return false;
            }

            return true;
        }

        // =========================
        // CLEAR
        // =========================
        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();

            nudPrice.Value = 0;
            nudStock.Value = 0;

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            txtBarcode.Focus();
        }
    }
}
