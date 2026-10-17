using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DBContext _context;

        public OrdersController(DBContext context)
        {
            _context = context;
        }

        // POST /api/orders/checkout  (màn hình POS thanh toán)
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] OrderCheckoutRequestDto request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng đang trống!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId);

            var order = new Order
            {
                OrderDate = DateTime.Now,
                CashierUsername = request.CashierUsername
            };

            foreach (var item in request.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    return BadRequest(new { message = $"Sản phẩm ID {item.ProductId} không tồn tại!" });
                }

                if (item.Quantity <= 0 || product.StockQuantity < item.Quantity)
                {
                    return BadRequest(new { message = $"Sản phẩm '{product.ProductName}' không đủ tồn kho!" });
                }

                product.StockQuantity -= item.Quantity;
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price // Lấy giá từ Server, không tin giá Client gửi lên
                });
            }

            order.TotalAmount = order.OrderDetails.Sum(d => d.UnitPrice * d.Quantity);

            // Tích điểm cho khách hàng thân thiết: 10.000đ = 1 điểm
            if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhone.Trim());
                if (customer != null)
                {
                    order.CustomerId = customer.CustomerId;
                    customer.RewardPoints += (int)(order.TotalAmount / 10000);
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { orderId = order.OrderId, totalAmount = order.TotalAmount });
        }

        // GET /api/orders/report?date=2026-10-09  (Báo cáo doanh thu nhanh - Admin)
        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] DateTime date)
        {
            var from = date.Date;
            var to = from.AddDays(1);

            var orders = _context.Orders.AsNoTracking()
                .Where(o => o.OrderDate >= from && o.OrderDate < to);

            var totalOrders = await orders.CountAsync();
            var totalRevenue = await orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var bestSeller = await _context.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.OrderDate >= from && d.Order.OrderDate < to)
                .GroupBy(d => new { d.ProductId, d.Product!.ProductName })
                .Select(g => new { g.Key.ProductName, Quantity = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                date = from,
                totalOrders,
                totalRevenue,
                bestSellerName = bestSeller?.ProductName ?? string.Empty,
                bestSellerQuantity = bestSeller?.Quantity ?? 0
            });
        }
    }

    public class OrderCheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public List<OrderCheckoutItemDto> Items { get; set; } = new();
    }

    public class OrderCheckoutItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
