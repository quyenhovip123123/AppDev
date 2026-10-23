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
        private readonly AppDbContext _db;
        private readonly AdoNetRepository _ado;

        public OrdersController(AppDbContext db, AdoNetRepository ado)
        {
            _db = db;
            _ado = ado;
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

        // POST /api/orders — lập hóa đơn bán hàng: lưu hóa đơn, trừ tồn kho, đổi/cộng điểm trong 1 SqlTransaction (ADO.NET)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            int orderId;
            try
            {
                orderId = await _ado.CreateOrderAsync(User.GetUserId(), request);
            }
            catch (BusinessException ex)
            {
                // Transaction đã được Rollback bên trong repository
                return BadRequest(new { message = ex.Message });
            }

            var created = await Query().FirstAsync(o => o.OrderId == orderId);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, ToDto(created));
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

            // Trừ lại điểm đã cộng, hoàn lại điểm khách đã dùng
            if (order.Customer != null)
            {
                var earned = (int)(order.TotalAmount / AdoNetRepository.MoneyPerPoint);
                order.Customer.Points = Math.Max(0, order.Customer.Points - earned + order.PointsUsed);
            }

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
            PointsUsed = o.PointsUsed,
            PointsDiscount = o.PointsDiscount,
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
