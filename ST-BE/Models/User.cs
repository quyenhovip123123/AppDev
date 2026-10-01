namespace ST_BE.Models
{
    public static class Roles
    {
        public const string Admin = "Admin";   // Quản lý cửa hàng
        public const string Staff = "Staff";   // Nhân viên bán hàng

        public static bool IsValid(string role) => role == Admin || role == Staff;
    }

    // Tài khoản đăng nhập hệ thống
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;       // Lưu chữ thường

        public string PasswordHash { get; set; } = string.Empty;   // BCrypt hash, KHÔNG lưu mật khẩu gốc

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = Roles.Staff;

        public bool IsActive { get; set; } = true;

        // Mỗi lần đổi mật khẩu / khóa tài khoản / đổi quyền sẽ sinh stamp mới
        // -> mọi access token cũ (mang stamp cũ) lập tức bị từ chối
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString("N");

        public int FailedLoginCount { get; set; }

        public DateTime? LockoutEnd { get; set; }                  // UTC

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
