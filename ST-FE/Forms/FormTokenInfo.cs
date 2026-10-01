using ST_FE.Helpers;
using ST_FE.Services;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ST_FE.Forms
{
    // Hiển thị cấu trúc JWT đang dùng: Header, Payload (các claim) và thời hạn
    public partial class FormTokenInfo : Form
    {
        private static readonly JsonSerializerOptions PrettyJson = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public FormTokenInfo()
        {
            InitializeComponent();
            UiHelper.StyleButton(btnRefresh, UiHelper.Primary);
        }

        private void FormTokenInfo_Load(object sender, EventArgs e)
        {
            ShowToken();
        }

        private void ShowToken()
        {
            var token = Session.AccessToken;
            if (token == null)
            {
                txtRaw.Text = "(chưa đăng nhập)";
                txtDecoded.Clear();
                return;
            }

            txtRaw.Text = token;
            var parts = token.Split('.');

            var sb = new StringBuilder();
            sb.AppendLine("// HEADER — thuật toán ký và loại token");
            sb.AppendLine(Pretty(parts[0]));
            sb.AppendLine();
            sb.AppendLine("// PAYLOAD — các claim (thông tin người dùng) server ghi vào token");
            sb.AppendLine(Pretty(parts[1]));
            sb.AppendLine();
            sb.AppendLine("// Giải thích:");
            sb.AppendLine("//   nameid       = mã người dùng (UserId)");
            sb.AppendLine("//   unique_name  = tên đăng nhập");
            sb.AppendLine("//   role         = quyền: Admin (Quản lý) / Staff (Nhân viên)");
            sb.AppendLine("//   stamp        = SecurityStamp, đổi khi đổi mật khẩu/khóa tài khoản -> token cũ mất hiệu lực");
            sb.AppendLine("//   jti          = mã định danh duy nhất của token");
            sb.AppendLine("//   iss / aud    = bên phát hành / bên được phép dùng token");

            using (var doc = JsonDocument.Parse(Base64UrlDecode(parts[1])))
            {
                foreach (var claim in new[] { ("iat", "Thời điểm cấp"), ("nbf", "Có hiệu lực từ"), ("exp", "Hết hạn lúc") })
                {
                    if (doc.RootElement.TryGetProperty(claim.Item1, out var v))
                        sb.AppendLine($"//   {claim.Item1,-12} = {claim.Item2}: {DateTimeOffset.FromUnixTimeSeconds(v.GetInt64()).LocalDateTime:dd/MM/yyyy HH:mm:ss}");
                }
            }
            sb.AppendLine();
            sb.AppendLine("// SIGNATURE = HMACSHA256(base64Url(header) + \".\" + base64Url(payload), secret)");
            sb.AppendLine("//   " + parts[2]);

            txtDecoded.Text = sb.ToString();
            lblRefreshInfo.Text = $"Refresh token hết hạn lúc: {Session.RefreshTokenExpiresAt.ToLocalTime():dd/MM/yyyy HH:mm}\n" +
                                  $"Access token hết hạn lúc: {Session.AccessTokenExpiresAt.ToLocalTime():HH:mm:ss}";
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await UiHelper.RunBusyAsync(this, async () =>
            {
                // Gửi refresh token cũ -> nhận cặp token mới, refresh token cũ bị thu hồi (rotation)
                await ApiClient.RefreshNowAsync();
                ShowToken();
                UiHelper.ShowInfo("Đã nhận access token và refresh token mới.\nRefresh token cũ đã bị thu hồi trên server.");
            });
        }

        private static string Pretty(string base64Url)
        {
            using var doc = JsonDocument.Parse(Base64UrlDecode(base64Url));
            return JsonSerializer.Serialize(doc.RootElement, PrettyJson);
        }

        private static string Base64UrlDecode(string input)
        {
            var s = input.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
    }
}
