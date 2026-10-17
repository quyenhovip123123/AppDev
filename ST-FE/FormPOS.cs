using ST_FE.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace ST_FE
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá", Width = 110, DefaultCellStyle = { Format = "N0" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 70 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Thành Tiền", Width = 120, DefaultCellStyle = { Format = "N0" } });
        }

        private void FormPOS_Load(object sender, EventArgs e)
        {
            UpdateCartDisplay();
            txtBarcode.Focus();
        }

        // Phím tắt F9 = Thanh toán (hoạt động cả khi Form được nhúng trong Shell)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnCheckout.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Bắt sự kiện quét mã Barcode
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                e.SuppressKeyPress = true;
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Gọi API tra cứu sản phẩm theo Barcode
                var response = await ApiClientService.Client.GetAsync($"products/barcode/{Uri.EscapeDataString(barcode)}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                response.EnsureSuccessStatusCode();

                var product = await response.Content.ReadFromJsonAsync<ProductDto>();
                if (product == null)
                    return;

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                int newQuantity = (existingItem?.Quantity ?? 0) + 1;
                if (newQuantity > product.StockQuantity)
                {
                    MessageBox.Show($"Sản phẩm '{product.ProductName}' chỉ còn {product.StockQuantity} trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.TotalPrice);
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                decimal change = cashReceived - total;
                lblChange.Text = change >= 0 ? $"{change:N0} đ" : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
            }
        }

        // Tra cứu khách hàng thân thiết theo SĐT (Enter)
        private async void txtCustomerPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            string phone = txtCustomerPhone.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                lblCustomerName.Text = "Khách vãng lai";
                return;
            }

            try
            {
                var customers = await ApiClientService.Client
                    .GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(phone)}");
                var customer = customers?.FirstOrDefault(c => c.PhoneNumber == phone);

                lblCustomerName.Text = customer != null
                    ? $"{customer.CustomerName} - Hạng {customer.MembershipRank} ({customer.RewardPoints} điểm)"
                    : "Không tìm thấy - Khách vãng lai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _cart.Sum(x => x.TotalPrice);
            if (!decimal.TryParse(txtCashReceived.Text, out decimal cash) || cash < total)
            {
                MessageBox.Show("Số tiền khách đưa chưa đủ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCashReceived.Focus();
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            try
            {
                btnCheckout.Enabled = false;
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thanh toán thành công và đã in hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetPos();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Thanh toán thất bại từ máy chủ!\n" + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
                return;

            if (MessageBox.Show("Hủy toàn bộ giỏ hàng hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetPos();
            }
        }

        private void ResetPos()
        {
            _cart.Clear();
            UpdateCartDisplay();
            txtCashReceived.Clear();
            txtCustomerPhone.Clear();
            lblCustomerName.Text = "Khách vãng lai";
            txtBarcode.Focus();
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
