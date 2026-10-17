using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DBContext _context;

        public CustomersController(DBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.CustomerId)
                .ToListAsync();

            return Ok(customers);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customers>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng."
                });
            }

            return Ok(customer);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customers>>> SearchCustomers(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Từ khóa tìm kiếm không được để trống."
                });
            }

            keyword = keyword.Trim();

            var customers = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .OrderBy(c => c.CustomerName)
                .ToListAsync();

            return Ok(customers);
        }
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateCustomer(
            [FromBody] Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // Kiểm tra số điện thoại đã tồn tại
            var phoneExists = await _context.Customers
                .AnyAsync(c => c.PhoneNumber == customer.PhoneNumber);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại này đã tồn tại."
                });
            }

            // Đảm bảo ID được database tự sinh
            customer.CustomerId = 0;

            // Giá trị mặc định
            customer.RewardPoints = 0;

            if (string.IsNullOrWhiteSpace(customer.MembershipRank))
            {
                customer.MembershipRank = "Chuẩn";
            }

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.CustomerId },
                customer);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(
            int id,
            [FromBody] Customers customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest(new
                {
                    message = "ID trên URL không khớp với CustomerId."
                });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (existingCustomer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng."
                });
            }
            var phoneExists = await _context.Customers
                .AnyAsync(c =>
                    c.PhoneNumber == customer.PhoneNumber &&
                    c.CustomerId != id);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message = "Số điện thoại này đã được sử dụng."
                });
            }
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Address = customer.Address;
            existingCustomer.RewardPoints = customer.RewardPoints;
            existingCustomer.MembershipRank = customer.MembershipRank;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Cập nhật thông tin khách hàng thành công.",
                data = existingCustomer
            });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng."
                });
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Xóa khách hàng thành công."
            });
        }
    }
}
