using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Helpers;
using ST_BE.Models;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly string[] ValidRoles = { "Admin", "Cashier", "Warehouse" };
        private readonly DBContext _context;

        public UsersController(DBContext context)
        {
            _context = context;
        }

        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .ToListAsync();
            return Ok(users);
        }

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được trống!" });
            }

            if (!ValidRoles.Contains(dto.Role))
            {
                return BadRequest(new { message = "Vai trò không hợp lệ!" });
            }

            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return Conflict(new { message = "Tên đăng nhập đã tồn tại!" });
            }

            var user = new User
            {
                Username = dto.Username.Trim(),
                PasswordHash = PasswordHelper.Hash(dto.Password),
                FullName = dto.FullName.Trim(),
                Email = dto.Email,
                Role = dto.Role,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // PUT /api/users/{id}/reset-password  -> đặt lại mật khẩu mặc định 123456
        [HttpPut("{id:int}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản!" });
            }

            user.PasswordHash = PasswordHelper.Hash("123456");
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã đặt lại mật khẩu về 123456" });
        }

        // PUT /api/users/{id}/toggle-lock  -> khóa / mở khóa tài khoản
        [HttpPut("{id:int}/toggle-lock")]
        public async Task<IActionResult> ToggleLock(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản!" });
            }

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { isActive = user.IsActive });
        }
    }

    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Role { get; set; } = "Cashier";
    }
}
