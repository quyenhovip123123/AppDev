using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;
using ST_BE.Services;

namespace ST_BE.Controllers
{
    // Hóa đơn bán hàng. Nhân viên chỉ xem được hóa đơn do chính mình lập (lọc theo claim UserId trong JWT)
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private const decimal MoneyPerPoint = 10_000;
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        // GET /api/orders?from=2026-10-01&to=2026-10-31&keyword=HD...
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? keyword)
        {
            var query = Query();
            if (!User.IsAdmin())
            {
                var userId = User.GetUserId();
                query = query.Where(o => o.UserId == userId);
            }
            if (from.HasValue) query = query.Where(o => o.CreatedAt >= from.Value.Date);
            if (to.HasValue) query = query.Where(o => o.CreatedAt < to.Value.Date.AddDays(1));

            var list = (await query.OrderByDescending(o => o.CreatedAt).ToListAsync()).Select(ToDto);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                list = list.Where(o => o.OrderCode.Contains(k, StringComparison.OrdinalIgnoreCase)
                                    || o.CustomerName.Contains(k, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(list.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await Query().FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound(new { message = "Không tìm thấy hóa đơn" });
            if (!User.IsAdmin() && order.UserId != User.GetUserId())
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Bạn chỉ được xem hóa đơn do mình lập" });
            return Ok(ToDto(order));
        }

        // POST /api/orders — lập hóa đơn bán hàng: trừ tồn kho, cộng điểm khách hàng
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            // Gộp các dòng trùng mặt hàng
            var items = request.Items.GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) }).ToList();

            var ids = items.Select(i => i.ProductId).ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToDictionaryAsync(p => p.ProductId);

            Customer? customer = null;
            if (request.CustomerId.HasValue)
            {
                customer = await _db.Customers.FindAsync(request.CustomerId.Value);
                if (customer == null) return BadRequest(new { message = "Khách hàng không tồn tại" });
            }

            await using var tx = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                OrderCode = $"HD{DateTime.Now:yyMMddHHmmssfff}",
                CreatedAt = DateTime.Now,
                UserId = User.GetUserId(),
                CustomerId = customer?.CustomerId,
                Note = request.Note?.Trim(),
                Status = OrderStatus.Completed
            };

            foreach (var item in items)
            {
                if (!products.TryGetValue(item.ProductId, out var product) || !product.IsActive)
                    return BadRequest(new { message = $"Mặt hàng (ID {item.ProductId}) không tồn tại hoặc đã ngừng kinh doanh" });
                if (product.StockQuantity < item.Quantity)
                    return BadRequest(new { message = $"'{product.ProductName}' chỉ còn {product.StockQuantity} {product.Unit} trong kho" });

                product.StockQuantity -= item.Quantity;
                order.Details.Add(new OrderDetail
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.SalePrice,
                    UnitCost = product.CostPrice,
                    Quantity = item.Quantity,
                    LineTotal = product.SalePrice * item.Quantity
                });
            }

            order.SubTotal = order.Details.Sum(d => d.LineTotal);
            if (request.Discount > order.SubTotal)
                return BadRequest(new { message = "Số tiền giảm giá không được lớn hơn tổng tiền hàng" });

            order.Discount = request.Discount;
            order.TotalAmount = order.SubTotal - order.Discount;
            order.CustomerPaid = request.CustomerPaid;
            if (order.CustomerPaid < order.TotalAmount)
                return BadRequest(new { message = $"Khách đưa chưa đủ tiền (cần {order.TotalAmount:N0}đ)" });

            if (customer != null)
                customer.Points += (int)(order.TotalAmount / MoneyPerPoint);

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            var created = await Query().FirstAsync(o => o.OrderId == order.OrderId);
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, ToDto(created));
        }

        // POST /api/orders/5/cancel — hủy hóa đơn: hoàn tồn kho, trừ lại điểm (chỉ Admin)
        [HttpPost("{id}/cancel")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _db.Orders.Include(o => o.Details).ThenInclude(d => d.Product)
                                        .Include(o => o.Customer)
                                        .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound(new { message = "Không tìm thấy hóa đơn" });
            if (order.Status == OrderStatus.Cancelled) return BadRequest(new { message = "Hóa đơn đã bị hủy trước đó" });

            foreach (var d in order.Details)
                d.Product!.StockQuantity += d.Quantity;

            if (order.Customer != null)
                order.Customer.Points = Math.Max(0, order.Customer.Points - (int)(order.TotalAmount / MoneyPerPoint));

            order.Status = OrderStatus.Cancelled;
            await _db.SaveChangesAsync();
            return Ok(new { message = $"Đã hủy hóa đơn {order.OrderCode}" });
        }

        private IQueryable<Order> Query() =>
            _db.Orders.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Customer)
                .Include(o => o.Details).ThenInclude(d => d.Product);

        private static OrderDto ToDto(Order o) => new()
        {
            OrderId = o.OrderId,
            OrderCode = o.OrderCode,
            CreatedAt = o.CreatedAt,
            StaffName = o.User?.FullName ?? string.Empty,
            CustomerId = o.CustomerId,
            CustomerName = o.Customer?.FullName ?? "Khách lẻ",
            SubTotal = o.SubTotal,
            Discount = o.Discount,
            TotalAmount = o.TotalAmount,
            CustomerPaid = o.CustomerPaid,
            ChangeAmount = o.CustomerPaid - o.TotalAmount,
            Note = o.Note,
            Status = o.Status,
            Details = o.Details.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductId,
                ProductCode = d.Product?.ProductCode ?? string.Empty,
                ProductName = d.ProductName,
                UnitPrice = d.UnitPrice,
                Quantity = d.Quantity,
                LineTotal = d.LineTotal
            }).ToList()
        };
    }
}
