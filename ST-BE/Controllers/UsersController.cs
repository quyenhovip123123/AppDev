using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;
using ST_BE.Services;

namespace ST_BE.Controllers
{
    // Quản lý tài khoản nhân viên — chỉ Quản lý (Admin)
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.Users.AsNoTracking().OrderBy(u => u.UserId).ToListAsync();
            return Ok(users.Select(ToDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (!Roles.IsValid(request.Role))
                return BadRequest(new { message = "Quyền không hợp lệ (Admin hoặc Staff)" });

            var username = request.Username.Trim().ToLower();
            if (await _db.Users.AnyAsync(u => u.Username == username))
                return Conflict(new { message = "Tên đăng nhập đã tồn tại" });

            var user = new User
            {
                Username = username,
                FullName = request.FullName.Trim(),
                Role = request.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(ToDto(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (!Roles.IsValid(request.Role))
                return BadRequest(new { message = "Quyền không hợp lệ (Admin hoặc Staff)" });

            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản" });

            if (id == User.GetUserId() && (!request.IsActive || request.Role != Roles.Admin))
                return BadRequest(new { message = "Không thể tự khóa hoặc hạ quyền tài khoản đang đăng nhập" });

            // Đổi quyền hoặc khóa tài khoản => token cũ (chứa quyền cũ) phải hết hiệu lực ngay
            var securityChanged = user.Role != request.Role || user.IsActive != request.IsActive;

            user.FullName = request.FullName.Trim();
            user.Role = request.Role;
            user.IsActive = request.IsActive;

            if (securityChanged)
                await RevokeSessionsAsync(user);

            await _db.SaveChangesAsync();
            return Ok(ToDto(user));
        }

        [HttpPost("{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordRequest request)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản" });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.FailedLoginCount = 0;
            user.LockoutEnd = null;
            await RevokeSessionsAsync(user);
            await _db.SaveChangesAsync();
            return Ok(new { message = $"Đã đặt lại mật khẩu cho tài khoản {user.Username}" });
        }

        // Mở khóa tài khoản bị tạm khóa do đăng nhập sai nhiều lần
        [HttpPost("{id}/unlock")]
        public async Task<IActionResult> Unlock(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản" });

            user.FailedLoginCount = 0;
            user.LockoutEnd = null;
            await _db.SaveChangesAsync();
            return Ok(new { message = $"Đã mở khóa tài khoản {user.Username}" });
        }

        private async Task RevokeSessionsAsync(User user)
        {
            user.SecurityStamp = Guid.NewGuid().ToString("N");
            var tokens = await _db.RefreshTokens.Where(t => t.UserId == user.UserId && t.RevokedAt == null).ToListAsync();
            foreach (var t in tokens) t.RevokedAt = DateTime.UtcNow;
        }

        private static UserDto ToDto(User u) => new()
        {
            UserId = u.UserId,
            Username = u.Username,
            FullName = u.FullName,
            Role = u.Role,
            IsActive = u.IsActive,
            IsLockedOut = u.LockoutEnd > DateTime.UtcNow,
            CreatedAt = u.CreatedAt
        };
    }
}
