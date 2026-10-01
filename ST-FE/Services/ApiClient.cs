using ST_FE.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ST_FE.Services
{
    // Lớp duy nhất gọi Web API. Các Form chỉ việc gọi GetAsync/PostAsync/PutAsync/DeleteAsync
    public static class ApiClient
    {
        // Địa chỉ ST-BE (profile "https" trong launchSettings.json).
        // Có thể đổi bằng biến môi trường SUNNY_API_URL, VD: http://192.168.1.10:5124/api/
        public static readonly string BaseUrl =
            Environment.GetEnvironmentVariable("SUNNY_API_URL") ?? "https://localhost:7123/api/";

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        // Client chính: có AuthHandler gắn token + tự refresh
        private static readonly HttpClient Http = new(new AuthHandler())
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        // Client riêng cho login/refresh (không qua AuthHandler để tránh vòng lặp)
        private static readonly HttpClient AuthHttp = new()
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        private static readonly SemaphoreSlim RefreshLock = new(1, 1);
        private static bool _sessionExpiredRaised;

        // Phát ra khi refresh token cũng hết hạn/bị thu hồi -> giao diện phải quay về màn hình đăng nhập
        public static event EventHandler? SessionExpired;

        // ===== Xác thực =====

        public static async Task LoginAsync(string username, string password)
        {
            var response = await SendSafeAsync(() => AuthHttp.PostAsJsonAsync("auth/login", new { username, password }, JsonOptions));
            await EnsureSuccessAsync(response, authenticated: false);
            var auth = await response.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
            Session.Set(auth!);
            _sessionExpiredRaised = false;
        }

        public static async Task LogoutAsync()
        {
            _sessionExpiredRaised = true;   // chủ động đăng xuất, không báo "hết phiên"
            try
            {
                if (Session.RefreshToken != null)
                    await Http.PostAsJsonAsync("auth/logout", new { refreshToken = Session.RefreshToken }, JsonOptions);
            }
            catch
            {
                // Bỏ qua lỗi mạng khi đăng xuất, phía client vẫn xóa phiên
            }
            Session.Clear();
        }

        public static async Task ChangePasswordAsync(string currentPassword, string newPassword)
        {
            var auth = await PostAsync<AuthResponse>("auth/change-password", new { currentPassword, newPassword });
            Session.Set(auth);   // Server cấp token mới vì token cũ đã bị thu hồi
        }

        // Đổi refresh token lấy cặp token mới. failedToken = access token vừa bị từ chối,
        // dùng để biết luồng khác đã refresh xong hay chưa (tránh refresh 2 lần cùng lúc)
        internal static async Task<bool> TryRefreshAsync(string? failedToken)
        {
            await RefreshLock.WaitAsync();
            try
            {
                if (Session.AccessToken != null && Session.AccessToken != failedToken) return true;
                if (Session.RefreshToken == null) return false;

                var response = await AuthHttp.PostAsJsonAsync("auth/refresh", new { refreshToken = Session.RefreshToken }, JsonOptions);
                if (!response.IsSuccessStatusCode)
                {
                    Session.Clear();
                    return false;
                }
                Session.Set((await response.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions))!);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                RefreshLock.Release();
            }
        }

        // Chủ động làm mới token (nút "Làm mới token" trên màn hình Phiên đăng nhập)
        public static async Task RefreshNowAsync()
        {
            if (!await TryRefreshAsync(Session.AccessToken))
            {
                RaiseSessionExpired();
                throw new ApiException(HttpStatusCode.Unauthorized, "Không thể làm mới token, vui lòng đăng nhập lại", isSessionExpired: true);
            }
        }

        internal static void RaiseSessionExpired()
        {
            if (_sessionExpiredRaised) return;
            _sessionExpiredRaised = true;
            Session.Clear();
            SessionExpired?.Invoke(null, EventArgs.Empty);
        }

        // ===== Gọi API chung =====

        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await SendSafeAsync(() => Http.GetAsync(url));
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<T>(JsonOptions))!;
        }

        public static async Task<T> PostAsync<T>(string url, object? body)
        {
            var response = await SendSafeAsync(() => Http.PostAsJsonAsync(url, body, JsonOptions));
            await EnsureSuccessAsync(response);
            return (await response.Content.ReadFromJsonAsync<T>(JsonOptions))!;
        }

        // Trả về thông điệp "message" của server (nếu có)
        public static async Task<string?> PostAsync(string url, object? body = null)
        {
            var response = await SendSafeAsync(() => Http.PostAsJsonAsync(url, body ?? new { }, JsonOptions));
            await EnsureSuccessAsync(response);
            return await ReadMessageAsync(response);
        }

        public static async Task PutAsync(string url, object body)
        {
            var response = await SendSafeAsync(() => Http.PutAsJsonAsync(url, body, JsonOptions));
            await EnsureSuccessAsync(response);
        }

        public static async Task<string?> DeleteAsync(string url)
        {
            var response = await SendSafeAsync(() => Http.DeleteAsync(url));
            await EnsureSuccessAsync(response);
            return await ReadMessageAsync(response);
        }

        // ===== Xử lý lỗi =====

        private static async Task<HttpResponseMessage> SendSafeAsync(Func<Task<HttpResponseMessage>> send)
        {
            try
            {
                return await send();
            }
            catch (HttpRequestException)
            {
                throw new ApiException(null, $"Không kết nối được tới máy chủ ({BaseUrl}).\nHãy kiểm tra ST-BE đã được chạy chưa.");
            }
            catch (TaskCanceledException)
            {
                throw new ApiException(null, "Máy chủ phản hồi quá lâu, vui lòng thử lại.");
            }
        }

        private static async Task EnsureSuccessAsync(HttpResponseMessage response, bool authenticated = true)
        {
            if (response.IsSuccessStatusCode) return;

            var message = await ReadMessageAsync(response);
            message ??= response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => "Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại",
                HttpStatusCode.Forbidden => "Bạn không có quyền thực hiện chức năng này",
                HttpStatusCode.NotFound => "Không tìm thấy dữ liệu",
                _ => $"Lỗi máy chủ ({(int)response.StatusCode})"
            };
            throw new ApiException(response.StatusCode, message,
                isSessionExpired: authenticated && response.StatusCode == HttpStatusCode.Unauthorized);
        }

        // Đọc { "message": "..." } hoặc lỗi kiểm tra dữ liệu { "errors": { "Field": ["..."] } }
        private static async Task<string?> ReadMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var text = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(text)) return null;

                using var doc = JsonDocument.Parse(text);
                var root = doc.RootElement;
                if (root.ValueKind != JsonValueKind.Object) return null;

                if (root.TryGetProperty("message", out var msg)) return msg.GetString();

                if (root.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Object)
                {
                    var lines = errors.EnumerateObject()
                        .SelectMany(e => e.Value.EnumerateArray().Select(v => v.GetString()))
                        .Where(s => !string.IsNullOrEmpty(s));
                    return string.Join("\n", lines);
                }

                if (root.TryGetProperty("title", out var title)) return title.GetString();
            }
            catch (JsonException)
            {
            }
            return null;
        }
    }
}
