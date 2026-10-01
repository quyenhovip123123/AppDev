using ST_FE.Helpers;
using ST_FE.Models;
using ST_FE.Services;
using System.ComponentModel;

namespace ST_FE.Forms
{
    // Màn hình bán hàng tại quầy (POS)
    public partial class FormSales : Form
    {
        private List<ProductDto> _products = new();
        private readonly BindingList<CartItem> _cart = new();
        private CustomerDto? _customer;

        public FormSales()
        {
            InitializeComponent();

            UiHelper.SetupGrid(dgvProducts,
                new GridColumn(nameof(ProductDto.ProductCode), "Mã hàng", 70),
                new GridColumn(nameof(ProductDto.ProductName), "Tên hàng", 200),
                new GridColumn(nameof(ProductDto.Unit), "ĐVT", 50),
                new GridColumn(nameof(ProductDto.SalePrice), "Giá bán", 80, "N0"),
                new GridColumn(nameof(ProductDto.StockQuantity), "Tồn", 50, "N0"));

            UiHelper.SetupGrid(dgvCart,
                new GridColumn(nameof(CartItem.ProductName), "Mặt hàng", 170),
                new GridColumn(nameof(CartItem.Quantity), "SL", 40, "N0"),
                new GridColumn(nameof(CartItem.UnitPrice), "Đơn giá", 70, "N0"),
                new GridColumn(nameof(CartItem.LineTotal), "Thành tiền", 80, "N0"));
            dgvCart.DataSource = _cart;

            UiHelper.StyleButton(btnAddToCart, UiHelper.Primary);
            UiHelper.StyleButton(btnCheckout, UiHelper.Success);
            btnCheckout.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            UiHelper.StyleButton(btnRemoveItem, UiHelper.Danger);
            UiHelper.StyleButton(btnClearCart, UiHelper.Danger);
        }

        private async void FormSales_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
            txtProductSearch.Focus();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                _products = await ApiClient.GetAsync<List<ProductDto>>("products");
                FilterProducts();
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private void FilterProducts()
        {
            var k = txtProductSearch.Text.Trim();
            dgvProducts.DataSource = string.IsNullOrEmpty(k)
                ? _products
                : _products.Where(p => p.ProductName.Contains(k, StringComparison.OrdinalIgnoreCase)
                                    || p.ProductCode.Contains(k, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Enter: nếu gõ/quét đúng mã hàng thì thêm luôn vào giỏ, ngược lại lọc danh sách
        private void txtProductSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;

            var code = txtProductSearch.Text.Trim();
            var exact = _products.FirstOrDefault(p => p.ProductCode.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
            {
                AddToCart(exact, (int)nudQuantity.Value);
                txtProductSearch.Clear();
                FilterProducts();
            }
            else
            {
                FilterProducts();
            }
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnAddToCart.PerformClick();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            var product = dgvProducts.SelectedItem<ProductDto>();
            if (product == null)
            {
                UiHelper.ShowWarning("Vui lòng chọn mặt hàng!");
                return;
            }
            AddToCart(product, (int)nudQuantity.Value);
        }

        private void AddToCart(ProductDto product, int quantity)
        {
            var item = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
            var newQty = (item?.Quantity ?? 0) + quantity;
            if (newQty > product.StockQuantity)
            {
                UiHelper.ShowWarning($"'{product.ProductName}' chỉ còn {product.StockQuantity} {product.Unit} trong kho!");
                return;
            }

            if (item == null)
            {
                _cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    Unit = product.Unit,
                    UnitPrice = product.SalePrice,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity = newQty;
                _cart.ResetBindings();
            }
            nudQuantity.Value = 1;
            UpdateTotals();
        }

        private void ChangeSelectedQuantity(int delta)
        {
            var item = dgvCart.SelectedItem<CartItem>();
            if (item == null) return;

            var product = _products.FirstOrDefault(p => p.ProductId == item.ProductId);
            var newQty = item.Quantity + delta;
            if (product != null && newQty > product.StockQuantity)
            {
                UiHelper.ShowWarning($"Chỉ còn {product.StockQuantity} {product.Unit} trong kho!");
                return;
            }
            if (newQty <= 0)
                _cart.Remove(item);
            else
                item.Quantity = newQty;

            _cart.ResetBindings();
            UpdateTotals();
        }

        private void btnIncrease_Click(object sender, EventArgs e) => ChangeSelectedQuantity(1);
        private void btnDecrease_Click(object sender, EventArgs e) => ChangeSelectedQuantity(-1);

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            var item = dgvCart.SelectedItem<CartItem>();
            if (item == null) return;
            _cart.Remove(item);
            UpdateTotals();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0 && UiHelper.Confirm("Xóa toàn bộ giỏ hàng?")) ResetSale();
        }

        // ===== Khách hàng =====

        private async void btnFindCustomer_Click(object sender, EventArgs e)
        {
            var phone = txtCustomerPhone.Text.Trim();
            if (phone.Length == 0) return;

            try
            {
                var found = await ApiClient.GetAsync<List<CustomerDto>>($"customers?keyword={Uri.EscapeDataString(phone)}");
                var match = found.FirstOrDefault(c => c.Phone == phone) ?? (found.Count == 1 ? found[0] : null);
                if (match == null)
                {
                    UiHelper.ShowInfo("Không tìm thấy khách hàng. Có thể thêm mới ở mục Khách hàng.");
                    return;
                }
                SetCustomer(match);
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(ex);
            }
        }

        private void txtCustomerPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFindCustomer.PerformClick();
            }
        }

        private void btnRetailCustomer_Click(object sender, EventArgs e) => SetCustomer(null);

        private void SetCustomer(CustomerDto? customer)
        {
            _customer = customer;
            if (customer == null) txtCustomerPhone.Clear();
            lblCustomerInfo.Text = customer == null
                ? "Khách lẻ"
                : $"✔ {customer.FullName} — {customer.Phone} — Điểm tích lũy: {customer.Points:N0}";
        }

        // ===== Thanh toán =====

        private decimal SubTotal => _cart.Sum(c => c.LineTotal);
        private decimal Total => Math.Max(0, SubTotal - nudDiscount.Value);

        private void UpdateTotals()
        {
            lblSubTotal.Text = UiHelper.Money(SubTotal);
            lblTotal.Text = UiHelper.Money(Total);
            var change = nudCustomerPaid.Value - Total;
            lblChange.Text = change >= 0 ? UiHelper.Money(change) : "Chưa đủ: " + UiHelper.Money(-change);
            lblChange.ForeColor = change >= 0 ? UiHelper.Success : UiHelper.Danger;
            lblCartTitle.Text = $"🛒 Giỏ hàng ({_cart.Sum(c => c.Quantity)} sản phẩm)";
        }

        private void Totals_ValueChanged(object sender, EventArgs e) => UpdateTotals();

        private void btnExactCash_Click(object sender, EventArgs e)
        {
            nudCustomerPaid.Value = Math.Min(Total, nudCustomerPaid.Maximum);
        }

        private void FormSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                e.SuppressKeyPress = true;
                btnCheckout.PerformClick();
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                UiHelper.ShowWarning("Giỏ hàng đang trống!");
                return;
            }
            if (nudDiscount.Value > SubTotal)
            {
                UiHelper.ShowWarning("Giảm giá không được lớn hơn tổng tiền hàng!");
                return;
            }
            if (nudCustomerPaid.Value < Total)
            {
                UiHelper.ShowWarning($"Khách đưa chưa đủ tiền! Cần {UiHelper.Money(Total)}");
                nudCustomerPaid.Focus();
                return;
            }
            if (!UiHelper.Confirm($"Xác nhận thanh toán {UiHelper.Money(Total)}?")) return;

            await UiHelper.RunBusyAsync(this, async () =>
            {
                // POST /api/orders — server kiểm tra tồn kho, trừ kho và cộng điểm trong 1 transaction
                var order = await ApiClient.PostAsync<OrderDto>("orders", new
                {
                    CustomerId = _customer?.CustomerId,
                    Discount = nudDiscount.Value,
                    CustomerPaid = nudCustomerPaid.Value,
                    Note = txtNote.Text.Trim(),
                    Items = _cart.Select(c => new { c.ProductId, c.Quantity }).ToList()
                });

                ResetSale();
                await LoadProductsAsync();

                using var detail = new FormOrderDetail(order);
                detail.ShowDialog(this);
            });
        }

        private void ResetSale()
        {
            _cart.Clear();
            nudDiscount.Value = 0;
            nudCustomerPaid.Value = 0;
            txtNote.Clear();
            SetCustomer(null);
            UpdateTotals();
            txtProductSearch.Focus();
        }
    }
}
