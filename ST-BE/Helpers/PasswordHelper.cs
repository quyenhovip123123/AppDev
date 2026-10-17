using System.Security.Cryptography;
using System.Text;

namespace ST_BE.Helpers
{
    // Băm mật khẩu SHA256 (đủ dùng cho bài thực hành, thực tế nên dùng BCrypt/PBKDF2)
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public static bool Verify(string password, string hash)
        {
            return string.Equals(Hash(password), hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
