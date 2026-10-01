using ST_FE.Models;

namespace ST_FE.Services
{
    // Lưu phiên đăng nhập trong bộ nhớ (không ghi token ra đĩa để tránh bị đánh cắp)
    public static class Session
    {
        public static string? AccessToken { get; private set; }
        public static DateTime AccessTokenExpiresAt { get; private set; }
        public static string? RefreshToken { get; private set; }
        public static DateTime RefreshTokenExpiresAt { get; private set; }
        public static UserInfoDto? User { get; private set; }

        public static bool IsLoggedIn => AccessToken != null;
        public static bool IsAdmin => User?.Role == "Admin";
        public static string RoleDisplay => IsAdmin ? "Quản lý" : "Nhân viên bán hàng";

        public static void Set(AuthResponse auth)
        {
            AccessToken = auth.AccessToken;
            AccessTokenExpiresAt = auth.AccessTokenExpiresAt.ToUniversalTime();
            RefreshToken = auth.RefreshToken;
            RefreshTokenExpiresAt = auth.RefreshTokenExpiresAt.ToUniversalTime();
            User = auth.User;
        }

        public static void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
            User = null;
            AccessTokenExpiresAt = DateTime.MinValue;
        }
    }
}
