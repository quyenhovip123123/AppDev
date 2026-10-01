using System.Net;

namespace ST_FE.Services
{
    // Lỗi trả về từ API, đã được chuyển thành thông điệp tiếng Việt để hiển thị
    public class ApiException : Exception
    {
        public HttpStatusCode? StatusCode { get; }

        // true = lỗi 401 khi đang dùng token (phiên hết hạn/bị thu hồi), khác với 401 do nhập sai mật khẩu lúc đăng nhập
        public bool IsSessionExpired { get; }

        public ApiException(HttpStatusCode? statusCode, string message, bool isSessionExpired = false) : base(message)
        {
            StatusCode = statusCode;
            IsSessionExpired = isSessionExpired;
        }
    }
}
