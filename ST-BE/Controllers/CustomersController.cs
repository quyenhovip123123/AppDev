using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    // Nhân viên được xem/thêm/sửa khách hàng (phục vụ bán hàng). Xóa: chỉ Admin
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CustomersController(AppDbContext db)
        {
            _db = db;
        }

        // GET /api/customers?keyword=090 (tìm theo tên hoặc số điện thoại)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var list = await _db.Customers.AsNoTracking().OrderBy(c => c.CustomerId).ToListAsync();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                list = list.Where(c => c.FullName.Contains(k, StringComparison.OrdinalIgnoreCase) || c.Phone.Contains(k)).ToList();
            }
            return Ok(list.Select(ToDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _db.Customers.FindAsync(id);
            if (c == null) return NotFound(new { message = "Không tìm thấy khách hàng" });
            return Ok(ToDto(c));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequest request)
        {
            var phone = request.Phone.Trim();
            if (await _db.Customers.AnyAsync(c => c.Phone == phone))
                return Conflict(new { message = "Số điện thoại đã được đăng ký" });

            var customer = new Customer();
            Apply(customer, request);
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, ToDto(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest request)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return NotFound(new { message = "Không tìm thấy khách hàng" });

            var phone = request.Phone.Trim();
            if (await _db.Customers.AnyAsync(c => c.Phone == phone && c.CustomerId != id))
                return Conflict(new { message = "Số điện thoại đã được đăng ký" });

            Apply(customer, request);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return NotFound(new { message = "Không tìm thấy khách hàng" });

            if (await _db.Orders.AnyAsync(o => o.CustomerId == id))
                return BadRequest(new { message = "Khách hàng đã có hóa đơn, không thể xóa" });

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private static void Apply(Customer c, CustomerRequest r)
        {
            c.FullName = r.FullName.Trim();
            c.Phone = r.Phone.Trim();
            c.Email = string.IsNullOrWhiteSpace(r.Email) ? null : r.Email.Trim();
            c.Address = r.Address?.Trim();
        }

        private static CustomerDto ToDto(Customer c) => new()
        {
            CustomerId = c.CustomerId,
            FullName = c.FullName,
            Phone = c.Phone,
            Email = c.Email,
            Address = c.Address,
            Points = c.Points,
            CreatedAt = c.CreatedAt
        };
    }
}
