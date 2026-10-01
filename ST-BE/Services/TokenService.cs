using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ST_BE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ST_BE.Services
{
    public static class AppClaims
    {
        public const string FullName = "full_name";
        public const string SecurityStamp = "stamp";
    }

    public class TokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        // Tạo Access Token (JWT) ký bằng HMAC-SHA256
        // Header.Payload.Signature — payload chứa các claim: id, username, họ tên, quyền, stamp
        public (string Token, DateTime ExpiresAt) CreateAccessToken(User user)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(AppClaims.FullName, user.FullName),
                    new Claim(AppClaims.SecurityStamp, user.SecurityStamp),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            return (handler.WriteToken(handler.CreateToken(descriptor)), expiresAt);
        }

        // Refresh token là chuỗi ngẫu nhiên 64 byte (không phải JWT), sinh bằng bộ sinh số ngẫu nhiên an toàn mật mã
        public (string RawToken, RefreshToken Entity) CreateRefreshToken(int userId, string? ip)
        {
            var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var entity = new RefreshToken
            {
                UserId = userId,
                TokenHash = HashToken(raw),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenDays),
                CreatedByIp = ip
            };
            return (raw, entity);
        }

        public static string HashToken(string rawToken) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawToken)));
    }
}
