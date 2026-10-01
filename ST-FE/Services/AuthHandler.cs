using System.Net;
using System.Net.Http.Headers;

namespace ST_FE.Services
{
    // Chặn mọi request gửi đi để:
    //  1. Tự gắn header "Authorization: Bearer <access token>"
    //  2. Tự làm mới token khi sắp hết hạn hoặc khi server trả 401, rồi gửi lại request
    public class AuthHandler : DelegatingHandler
    {
        public AuthHandler() : base(new HttpClientHandler()) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Đọc trước nội dung để có thể gửi lại request nếu cần
            byte[]? body = request.Content == null ? null : await request.Content.ReadAsByteArrayAsync(cancellationToken);

            // Làm mới chủ động nếu access token còn dưới 30 giây
            if (Session.IsLoggedIn && Session.AccessTokenExpiresAt <= DateTime.UtcNow.AddSeconds(30))
                await ApiClient.TryRefreshAsync(Session.AccessToken);

            var tokenUsed = Session.AccessToken;
            AttachToken(request, tokenUsed);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Unauthorized || tokenUsed == null)
                return response;

            // 401: thử đổi refresh token lấy access token mới rồi gửi lại 1 lần
            if (await ApiClient.TryRefreshAsync(tokenUsed))
            {
                response.Dispose();
                var retry = Clone(request, body);
                AttachToken(retry, Session.AccessToken);
                return await base.SendAsync(retry, cancellationToken);
            }

            ApiClient.RaiseSessionExpired();
            return response;
        }

        private static void AttachToken(HttpRequestMessage request, string? token)
        {
            request.Headers.Authorization = token == null ? null : new AuthenticationHeaderValue("Bearer", token);
        }

        private static HttpRequestMessage Clone(HttpRequestMessage original, byte[]? body)
        {
            var clone = new HttpRequestMessage(original.Method, original.RequestUri);
            foreach (var header in original.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            if (body != null)
            {
                clone.Content = new ByteArrayContent(body);
                foreach (var header in original.Content!.Headers)
                    clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            return clone;
        }
    }
}
