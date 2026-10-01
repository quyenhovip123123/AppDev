namespace ST_BE.Models
{
    // Refresh token dùng để xin access token mới khi access token hết hạn.
    // Chỉ lưu giá trị băm SHA-256 -> lộ CSDL cũng không dùng được token.
    public class RefreshToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public string TokenHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }                    // UTC

        public DateTime ExpiresAt { get; set; }                    // UTC

        public DateTime? RevokedAt { get; set; }                   // UTC

        public string? ReplacedByTokenHash { get; set; }           // Token mới sinh ra khi xoay vòng (rotation)

        public string? CreatedByIp { get; set; }

        public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
    }
}
