using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST_BE.Data;
using ST_BE.Dtos;
using ST_BE.Models;
using ST_BE.Services;

namespace ST_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext db, TokenService tokenService, IConfiguration configuration)
        {
            _db = db;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        // POST /api/auth/login — đăng nhập, trả về access token + refresh token
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var username = request.Username.Trim().ToLower();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Không tiết lộ "sai tên" hay "sai mật khẩu" để tránh dò tài khoản
            if (user == null)
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                var minutes = Math.Ceiling((user.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes);
                return Unauthorized(new { message = $"Tài khoản tạm khóa do đăng nhập sai nhiều lần. Vui lòng thử lại sau {minutes} phút" });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                var maxAttempts = _configuration.GetValue("LoginLockout:MaxFailedAttempts", 5);
                user.FailedLoginCount++;
                if (user.FailedLoginCount >= maxAttempts)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(_configuration.GetValue("LoginLockout:LockoutMinutes", 5));
                    user.FailedLoginCount = 0;
                }
                await _db.SaveChangesAsync();
                return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu" });
            }

            if (!user.IsActive)
                return Unauthorized(new { message = "Tài khoản đã bị vô hiệu hóa, vui lòng liên hệ quản lý" });

            user.FailedLoginCount = 0;
            user.LockoutEnd = null;

            return Ok(await IssueTokensAsync(user));
        }

        // POST /api/auth/refresh — đổi refresh token cũ lấy cặp token mới (Refresh Token Rotation)
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var hash = TokenService.HashToken(request.RefreshToken);
            var stored = await _db.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.TokenHash == hash);

            if (stored == null)
                return Unauthorized(new { message = "Refresh token không hợp lệ" });

            // Token đã bị thu hồi mà vẫn được dùng lại => có thể đã bị đánh cắp.
            // Thu hồi toàn bộ phiên của tài khoản này để bảo vệ người dùng.
            if (stored.RevokedAt != null)
            {
                stored.User!.SecurityStamp = Guid.NewGuid().ToString("N");
                await RevokeAllAsync(stored.UserId);
                await _db.SaveChangesAsync();
                return Unauthorized(new { message = "Phát hiện refresh token bị dùng lại. Mọi phiên đăng nhập đã bị thu hồi" });
            }

            if (stored.ExpiresAt <= DateTime.UtcNow)
                return Unauthorized(new { message = "Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại" });

            var user = stored.User!;
            if (!user.IsActive)
                return Unauthorized(new { message = "Tài khoản đã bị vô hiệu hóa" });

            var response = await IssueTokensAsync(user, stored);
            return Ok(response);
        }

        // POST /api/auth/logout — thu hồi refresh token của phiên hiện tại
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            var hash = TokenService.HashToken(request.RefreshToken);
            var userId = User.GetUserId();
            var stored = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == hash && t.UserId == userId);
            if (stored != null && stored.RevokedAt == null)
            {
                stored.RevokedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }
            return Ok(new { message = "Đã đăng xuất" });
        }

        // GET /api/auth/me — thông tin tài khoản đang đăng nhập (đọc từ claim trong token)
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == User.GetUserId());
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản" });
            return Ok(ToInfo(user));
        }

        // POST /api/auth/change-password — đổi mật khẩu, đăng xuất khỏi mọi thiết bị khác
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == User.GetUserId());
            if (user == null) return NotFound(new { message = "Không tìm thấy tài khoản" });

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                return BadRequest(new { message = "Mật khẩu hiện tại không đúng" });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.SecurityStamp = Guid.NewGuid().ToString("N");   // vô hiệu mọi access token cũ
            await RevokeAllAsync(user.UserId);                     // vô hiệu mọi refresh token cũ

            // Cấp lại token mới cho phiên hiện tại để người dùng không bị đăng xuất
            return Ok(await IssueTokensAsync(user));
        }

        private async Task<AuthResponse> IssueTokensAsync(User user, RefreshToken? replacing = null)
        {
            var (accessToken, accessExpires) = _tokenService.CreateAccessToken(user);
            var (rawRefresh, refreshEntity) = _tokenService.CreateRefreshToken(user.UserId, HttpContext.Connection.RemoteIpAddress?.ToString());

            if (replacing != null)
            {
                replacing.RevokedAt = DateTime.UtcNow;
                replacing.ReplacedByTokenHash = refreshEntity.TokenHash;
            }

            _db.RefreshTokens.Add(refreshEntity);

            // Dọn các refresh token đã hết hạn quá 1 ngày của user
            var cutoff = DateTime.UtcNow.AddDays(-1);
            _db.RefreshTokens.RemoveRange(_db.RefreshTokens.Where(t => t.UserId == user.UserId && t.ExpiresAt < cutoff));

            await _db.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = accessExpires,
                RefreshToken = rawRefresh,
                RefreshTokenExpiresAt = refreshEntity.ExpiresAt,
                User = ToInfo(user)
            };
        }

        private async Task RevokeAllAsync(int userId)
        {
            var active = await _db.RefreshTokens.Where(t => t.UserId == userId && t.RevokedAt == null).ToListAsync();
            foreach (var t in active) t.RevokedAt = DateTime.UtcNow;
        }

        private static UserInfoDto ToInfo(User u) => new()
        {
            UserId = u.UserId,
            Username = u.Username,
            FullName = u.FullName,
            Role = u.Role
        };
    }
}
