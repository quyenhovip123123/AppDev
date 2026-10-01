# ☀ Sunny Stationery — Hệ thống Quản lý Cửa hàng Bán lẻ áp dụng Cơ chế Bảo mật JWT

Hệ thống quản lý **Cửa hàng Văn phòng phẩm Sunny** theo mô hình Client – Server:

| Project | Vai trò | Công nghệ |
|---|---|---|
| `ST-BE` | Web API: nghiệp vụ, CSDL, xác thực & phân quyền | ASP.NET Core 8, EF Core 8 (SQLite / SQL Server), JWT Bearer, BCrypt, Swagger |
| `ST-FE` | Ứng dụng quầy bán hàng & quản lý | Windows Forms (.NET 8), `HttpClient` + `System.Net.Http.Json` |

---

## 1. Chức năng

| Chức năng | Quản lý (Admin) | Nhân viên (Staff) |
|---|:-:|:-:|
| Đăng nhập / đăng xuất / đổi mật khẩu / xem phiên JWT | ✅ | ✅ |
| Bán hàng (POS): tìm/quét mã hàng, giỏ hàng, giảm giá, tiền thừa, tích điểm, in hóa đơn | ✅ | ✅ |
| Xem hóa đơn | Tất cả | Chỉ hóa đơn mình lập |
| Hủy hóa đơn (hoàn kho, trừ điểm) | ✅ | ❌ |
| Mặt hàng, Nhóm hàng | Thêm/sửa/xóa | Chỉ xem (không thấy giá nhập) |
| Khách hàng | Thêm/sửa/xóa | Thêm/sửa |
| Nhập kho (cộng tồn, cập nhật giá nhập) | ✅ | ❌ |
| Tổng quan: doanh thu, lợi nhuận, biểu đồ theo ngày, top bán chạy, hàng sắp hết | ✅ | ❌ |
| Quản lý tài khoản nhân viên (tạo, khóa, đổi quyền, đặt lại mật khẩu, mở khóa) | ✅ | ❌ |

---

## 2. Cơ chế bảo mật JWT

```
 WinForms                                   Web API
    │  POST /api/auth/login {user, pass}        │  BCrypt.Verify(pass, hash)
    │ ─────────────────────────────────────────▶│  sai 5 lần → khóa 5 phút
    │  { accessToken (JWT, 15'), refreshToken (7 ngày) }
    │ ◀─────────────────────────────────────────│
    │                                           │
    │  GET /api/products                        │  1. Kiểm tra chữ ký HMAC-SHA256
    │  Authorization: Bearer <accessToken>      │  2. Kiểm tra iss, aud, exp
    │ ─────────────────────────────────────────▶│  3. Tài khoản còn hoạt động + SecurityStamp khớp
    │                                           │  4. [Authorize(Roles = "Admin")] nếu cần
    │  401 (token hết hạn)                      │
    │ ◀─────────────────────────────────────────│
    │  POST /api/auth/refresh {refreshToken}    │  Thu hồi refresh token cũ, cấp cặp mới (rotation)
    │ ─────────────────────────────────────────▶│
    │  gửi lại request ban đầu với token mới    │
```

**Phía server (`ST-BE`)**
- **Access token** là JWT ký HMAC-SHA256, sống 15 phút, chứa các claim: `nameid` (UserId), `unique_name`, `role`, `full_name`, `stamp`, `jti`. Chỉ chấp nhận thuật toán HS256, kiểm tra `iss`/`aud`/`exp`; secret phải dài ít nhất 32 byte.
- **Refresh token** là chuỗi ngẫu nhiên 64 byte. CSDL chỉ lưu **giá trị băm SHA-256** của nó.
- **Xoay vòng refresh token (rotation)**: mỗi lần refresh, token cũ bị thu hồi và cấp token mới. Nếu một token đã thu hồi bị **dùng lại** (dấu hiệu bị đánh cắp), server thu hồi **toàn bộ** phiên của tài khoản đó.
- **SecurityStamp**: khi đổi mật khẩu, khóa tài khoản, đổi quyền hoặc đặt lại mật khẩu, stamp được tạo mới, nên mọi access token cũ **mất hiệu lực ngay** mà không phải chờ hết hạn.
- **Mật khẩu** được băm bằng BCrypt. Đăng nhập sai 5 lần thì tài khoản bị khóa 5 phút (cấu hình trong `LoginLockout`). Thông báo lỗi không cho biết sai tên hay sai mật khẩu.
- **Phân quyền** bằng `[Authorize]` và `[Authorize(Roles = "Admin")]`. Nhân viên chỉ xem được hóa đơn của mình (lọc theo claim UserId) và không xem được giá nhập.
- Lỗi 401/403 luôn trả về JSON `{ "message": "..." }` bằng tiếng Việt.

**Phía client (`ST-FE`)**
- `Services/AuthHandler.cs` (một `DelegatingHandler`) tự gắn `Authorization: Bearer ...` vào mọi request. Nó **tự làm mới** token khi còn dưới 30 giây, hoặc khi server trả 401, rồi gửi lại request (kể cả POST/PUT có body).
- Khi nhiều request cùng gặp 401, `SemaphoreSlim` đảm bảo chỉ refresh **một lần**, tránh kích hoạt cơ chế phát hiện token bị dùng lại.
- Token chỉ nằm trong bộ nhớ (`Session`), không ghi ra đĩa. Đăng xuất hoặc đóng cửa sổ sẽ gọi `/api/auth/logout` để thu hồi refresh token.
- Khi refresh thất bại (hết hạn hoặc bị thu hồi), ứng dụng báo hết phiên và quay về màn hình đăng nhập.
- Menu **🔑 Phiên đăng nhập (JWT)** hiển thị Header/Payload/Signature của token đang dùng, giải thích từng claim và có nút làm mới token để minh họa rotation. Thanh trạng thái đếm ngược thời hạn access token.

---

## 3. Cách chạy

### Yêu cầu
- Visual Studio 2022 (workload *ASP.NET* và *.NET desktop*), hoặc .NET 8 SDK
- Windows để chạy WinForms

### Bước 1: Tin cậy chứng chỉ HTTPS dev (chỉ làm 1 lần)
```bash
dotnet dev-certs https --trust
```

### Bước 2: Chạy Backend
- **Visual Studio:** chuột phải Solution → *Configure Startup Projects* → *Multiple startup projects* → đặt `ST-BE` và `ST-FE` là **Start** (ST-BE xếp trước) → F5.
- **Dòng lệnh:**
  ```bash
  cd ST-BE
  dotnet run --launch-profile https
  ```
  Swagger: https://localhost:7123/swagger (bấm **Authorize** rồi dán access token để thử API).

Lần chạy đầu, hệ thống tự tạo CSDL `ST-BE/sunny_store.db` cùng dữ liệu mẫu (6 nhóm hàng, 20 mặt hàng, 3 khách hàng).

### Bước 3: Chạy WinForms
```bash
cd ST-FE
dotnet run
```

### Tài khoản mẫu
| Tên đăng nhập | Mật khẩu | Quyền |
|---|---|---|
| `admin` | `admin123` | Quản lý |
| `nhanvien` | `nhanvien123` | Nhân viên bán hàng |

### Tùy chọn
- **Dùng SQL Server:** trong `ST-BE/appsettings.json`, đặt `"DatabaseProvider": "SqlServer"` và sửa `ConnectionStrings:SqlServer`. CSDL được tạo tự động khi chạy.
- **Đổi địa chỉ API của client:** đặt biến môi trường `SUNNY_API_URL` (VD `http://192.168.1.10:5124/api/`). Mặc định là `https://localhost:7123/api/`.
- **Thời hạn token:** chỉnh `JwtSettings:AccessTokenMinutes` và `RefreshTokenDays`. Đặt `AccessTokenMinutes = 1` để quan sát cơ chế tự làm mới.
- **Triển khai thật:** không để `JwtSettings:Secret` trong `appsettings.json`; dùng biến môi trường `JwtSettings__Secret` hoặc *User Secrets*.

---

## 4. API chính

| Method | Endpoint | Quyền |
|---|---|---|
| POST | `/api/auth/login` · `/api/auth/refresh` | Không cần token |
| POST | `/api/auth/logout` · `/api/auth/change-password` · GET `/api/auth/me` | Đã đăng nhập |
| GET | `/api/categories` · `/api/categories/{id}` · `/api/categories/search?keyword=` | Đã đăng nhập |
| POST/PUT/DELETE | `/api/categories` | Admin |
| GET | `/api/products?keyword=&categoryId=&includeInactive=` · `/api/products/by-code/{code}` | Đã đăng nhập |
| POST/PUT/DELETE | `/api/products` | Admin |
| GET/POST/PUT | `/api/customers` | Đã đăng nhập |
| DELETE | `/api/customers/{id}` | Admin |
| GET/POST | `/api/orders?from=&to=&keyword=` | Đã đăng nhập (Staff chỉ thấy của mình) |
| POST | `/api/orders/{id}/cancel` | Admin |
| GET/POST | `/api/imports` | Admin |
| GET | `/api/reports/dashboard?from=&to=` | Admin |
| GET/POST/PUT | `/api/users` · POST `/api/users/{id}/reset-password` · `/api/users/{id}/unlock` | Admin |

---

## 5. Cấu trúc thư mục

```text
ST-BE/
├── Controllers/   Auth, Users, Categories, Products, Customers, Orders, Imports, Reports
├── Data/          AppDbContext (EF Core), DbSeeder (dữ liệu mẫu)
├── Dtos/          Đối tượng request/response
├── Models/        User, RefreshToken, Category, Product, Customer, Order, ImportReceipt
├── Services/      TokenService (tạo JWT/refresh token), JwtSettings, ClaimsExtensions
└── Program.cs     Cấu hình JWT Bearer, EF Core, Swagger

ST-FE/
├── Forms/         Login, Main, Dashboard, Sales (POS), Orders, OrderDetail, Products,
│                  CategoryManagement, Customers, Imports, Users, ChangePassword, TokenInfo
├── Services/      ApiClient, AuthHandler (gắn token + tự refresh), Session, ApiException
├── Models/        DTO phía client
└── Helpers/       UiHelper (màu sắc, DataGridView, thông báo)
```
