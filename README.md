# ☀ Sunny Stationery — Hệ thống Quản lý Cửa hàng Bán lẻ áp dụng Cơ chế Bảo mật JWT

Hệ thống quản lý **Cửa hàng Văn phòng phẩm Sunny** theo mô hình Client – Server:

| Project | Vai trò | Công nghệ |
|---|---|---|
| `ST-BE` | Web API: nghiệp vụ, CSDL, xác thực & phân quyền | ASP.NET Core 8, EF Core 8 + Migrations, ADO.NET (SqlClient), SQL Server, JWT Bearer, BCrypt, Swagger |
| `ST-FE` | Ứng dụng quầy bán hàng & quản lý | Windows Forms (.NET 8), `HttpClient` + `System.Net.Http.Json`, Chart (`WinForms.DataVisualization`) |

---

## 1. Chức năng

| Chức năng | Quản lý (Admin) | Bán hàng (Staff) | Kiểm kho (Warehouse) |
|---|:-:|:-:|:-:|
| Đăng nhập / đăng xuất / đổi mật khẩu / xem phiên JWT | ✅ | ✅ | ✅ |
| Bán hàng (POS): quét mã vạch EAN-13 (hoặc gõ mã hàng), giỏ hàng, giảm giá, **đổi điểm trừ tiền**, tiền thừa, tích điểm, in hóa đơn | ✅ | ✅ | ❌ |
| Xem hóa đơn | Tất cả | Chỉ hóa đơn mình lập | ❌ |
| Hủy hóa đơn (hoàn kho, trừ điểm đã cộng, hoàn điểm đã dùng) | ✅ | ❌ | ❌ |
| Mặt hàng (mã hàng + mã vạch riêng, tô đỏ hàng tồn ≤ 10) | Thêm/sửa/xóa | Chỉ xem (không thấy giá nhập) | Xem giá nhập + kiểm kê (sửa số tồn) |
| Nhóm hàng | Thêm/sửa/xóa | Chỉ xem | Chỉ xem |
| Xuất Excel (CSV): danh mục hàng kèm tồn kho / danh sách hóa đơn | ✅ | Chỉ hóa đơn | Chỉ danh mục hàng |
| Khách hàng | Thêm/sửa/xóa | Thêm/sửa | ❌ |
| Nhập kho (cộng tồn, cập nhật giá nhập) | ✅ | ❌ | ✅ |
| Tổng quan: 4 thẻ KPI, biểu đồ cột doanh thu theo ngày, top 5 bán chạy, hàng sắp hết | ✅ | ❌ | ❌ |
| Quản lý tài khoản nhân viên (tạo, khóa, đổi quyền, đặt lại mật khẩu, mở khóa) | ✅ | ❌ | ❌ |

Mọi danh sách (Mặt hàng, Nhóm hàng, Khách hàng, Hóa đơn, Lịch sử nhập kho, Tài khoản) đều **phân trang phía máy chủ** (15 dòng/trang).

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
- **Phân quyền** 3 vai trò (Admin / Staff / Warehouse) bằng `[Authorize]` và `[Authorize(Roles = "...")]`. Nhân viên chỉ xem được hóa đơn của mình (lọc theo claim UserId) và không xem được giá nhập.
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
- SQL Server: mặc định dùng **LocalDB** đi kèm Visual Studio (`(localdb)\MSSQLLocalDB`). Dùng SQL Server/Express khác thì sửa `ConnectionStrings:DefaultConnection` trong `ST-BE/appsettings.json`, VD `Server=localhost\\SQLEXPRESS;Database=SunnyStationeryDb;Trusted_Connection=True;TrustServerCertificate=True`
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

Khi khởi động, ST-BE tự áp dụng các **EF Core Migration** (`Database.Migrate()`) để tạo CSDL `SunnyStationeryDb`, các bảng, ràng buộc và Stored Procedure, sau đó nạp dữ liệu mẫu: 5 tài khoản, 12 nhóm hàng, 69 mặt hàng, 20 khách hàng, khoảng 22 phiếu nhập và 600 hóa đơn trong 75 ngày gần nhất (có mùa tựu trường, đơn hủy, đơn đổi điểm) để biểu đồ và báo cáo có số liệu. Dữ liệu mẫu chỉ được thêm phần còn thiếu, không ghi đè dữ liệu đã nhập.

Có thể tự tạo/cập nhật CSDL bằng lệnh:
```bash
dotnet tool restore                        # cài dotnet-ef (khai báo trong .config/dotnet-tools.json)
dotnet ef database update --project ST-BE  # = Update-Database trong Package Manager Console
# Thêm migration mới khi đổi Model:
dotnet ef migrations add <TenMigration> --project ST-BE -o Data/Migrations   # = Add-Migration
```

### Bước 3: Chạy WinForms
```bash
cd ST-FE
dotnet run
```

### Trường hợp máy cài khác phiên bản .NET
Cả hai project đều nhắm `net8.0`. Nếu máy chỉ có runtime mới hơn (VD .NET 9), `dotnet run` sẽ báo lỗi kiểu:
```text
You must install or update .NET to run this application.
Framework: 'Microsoft.AspNetCore.App', version '8.0.0' (x64)
```
Kiểm tra máy đang có gì:
```bash
dotnet --list-sdks       # SDK dùng để build
dotnet --list-runtimes   # runtime dùng để chạy (ST-BE cần Microsoft.AspNetCore.App, ST-FE cần Microsoft.WindowsDesktop.App)
```
- **Có SDK ≥ 8 nhưng thiếu runtime 8:** cho phép chạy trên runtime major mới hơn bằng biến môi trường `DOTNET_ROLL_FORWARD=Major` (không cần sửa code):
  ```bash
  # Linux / macOS / Git Bash
  DOTNET_ROLL_FORWARD=Major dotnet run
  ```
  ```powershell
  # PowerShell
  $env:DOTNET_ROLL_FORWARD = "Major"; dotnet run
  ```
  ```bat
  :: Command Prompt
  set "DOTNET_ROLL_FORWARD=Major" && dotnet run
  ```
  Muốn áp dụng luôn (kể cả khi bấm F5 trong Visual Studio), thêm `<RollForward>Major</RollForward>` vào `<PropertyGroup>` trong `ST-BE.csproj` / `ST-FE.csproj`.
- **Chỉ có SDK cũ hơn 8 (VD .NET 6, 7):** không build được `net8.0`. Cần cài [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), hoặc chỉ cài runtime: *ASP.NET Core Runtime 8* (cho ST-BE) và *.NET Desktop Runtime 8* (cho ST-FE).
- **Muốn chuyển hẳn lên .NET 9:** đổi `<TargetFramework>` thành `net9.0` (ST-FE: `net9.0-windows`) và nâng các gói `Microsoft.EntityFrameworkCore.*`, `Microsoft.AspNetCore.Authentication.JwtBearer` lên bản 9.x tương ứng.

### Tài khoản mẫu
| Tên đăng nhập | Mật khẩu | Quyền |
|---|---|---|
| `admin` | `admin123` | Quản lý |
| `nhanvien` | `nhanvien123` | Nhân viên bán hàng |
| `tuanpm`, `landt` | `nhanvien123` | Nhân viên bán hàng |
| `baohg` | `nhanvien123` | Nhân viên (đã khóa — minh họa tài khoản bị vô hiệu hóa) |
| `kiemkho` | `nhanvien123` | Nhân viên kiểm kho |

### Tùy chọn
- **Đổi địa chỉ API của client:** đặt biến môi trường `SUNNY_API_URL` (VD `https://localhost:7123/api/`). Mặc định là `http://localhost:5124/api/` (ở môi trường Development, BE không ép chuyển sang HTTPS nên không cần tin cậy chứng chỉ dev).
- **Thời hạn token:** chỉnh `JwtSettings:AccessTokenMinutes` và `RefreshTokenDays`. Đặt `AccessTokenMinutes = 1` để quan sát cơ chế tự làm mới.
- **Triển khai thật:** không để `JwtSettings:Secret` trong `appsettings.json`; dùng biến môi trường `JwtSettings__Secret` hoặc *User Secrets*.

---

## 4. API chính

| Method | Endpoint | Quyền |
|---|---|---|
| POST | `/api/auth/login` · `/api/auth/refresh` | Không cần token |
| POST | `/api/auth/logout` · `/api/auth/change-password` · GET `/api/auth/me` | Đã đăng nhập |
| GET | `/api/categories` · `/api/categories/paged?keyword=&page=&pageSize=` · `/api/categories/{id}` · `/api/categories/search?keyword=` | Đã đăng nhập |
| POST/PUT/DELETE | `/api/categories` | Admin |
| GET | `/api/products?keyword=&categoryId=&includeInactive=` · `/api/products/paged?page=&pageSize=` · `/api/products/by-barcode/{barcode}` = `/api/products/by-code/{code}` (ADO.NET, khớp mã vạch hoặc mã hàng) | Đã đăng nhập |
| POST/PUT/DELETE | `/api/products` | Admin |
| PUT | `/api/products/{id}/stock` (kiểm kê: sửa số tồn) | Admin, Warehouse |
| GET/POST/PUT | `/api/customers` · GET `/api/customers/paged?keyword=&page=&pageSize=` | Admin, Staff |
| DELETE | `/api/customers/{id}` | Admin |
| GET/POST | `/api/orders?from=&to=&keyword=` · GET `/api/orders/paged?...&page=&pageSize=` (POST chạy trong 1 `SqlTransaction`, có `redeemPoints`) | Admin, Staff (Staff chỉ thấy của mình) |
| POST | `/api/orders/{id}/cancel` | Admin |
| GET/POST | `/api/imports` · GET `/api/imports/paged?from=&to=&page=&pageSize=` | Admin, Warehouse |
| GET | `/api/reports/dashboard?from=&to=` · `/api/reports/dashboard-summary` · `/api/reports/top-products?top=5` | Admin |
| GET | `/api/reports/daily-revenue?from=&to=` (SP `sp_DailyRevenue`) · `/api/reports/shift-revenue?from=&to=&userId=` (SP `sp_RevenueByShift`) | Admin |
| GET/POST/PUT | `/api/users` · GET `/api/users/paged?page=&pageSize=` · POST `/api/users/{id}/reset-password` · `/api/users/{id}/unlock` | Admin |

---

## 5. Cấu trúc thư mục

```text
ST-BE/
├── Controllers/   Auth, Users, Categories, Products, Customers, Orders, Imports, Reports
├── Data/          AppDbContext (EF Core), DbSeeder (dữ liệu mẫu),
│                  AdoNetRepository (ADO.NET: mã vạch, thanh toán SqlTransaction, Stored Procedure, KPI),
│                  Migrations/ (InitialCreate, AddStoredProcedures, AddProductBarcode)
├── Dtos/          Đối tượng request/response
├── Models/        User, RefreshToken, Category, Product, Customer, Order, ImportReceipt
├── Services/      TokenService (tạo JWT/refresh token), JwtSettings, ClaimsExtensions
└── Program.cs     Cấu hình JWT Bearer, EF Core (SQL Server), CORS, Swagger

ST-FE/
├── Forms/         Login, Main, Dashboard, Sales (POS), Orders, OrderDetail, Products,
│                  CategoryManagement, Customers, Imports, Users, ChangePassword, TokenInfo
├── Services/      ApiClient, AuthHandler (gắn token + tự refresh), Session, ApiException
├── Models/        DTO phía client
└── Helpers/       UiHelper (màu sắc, DataGridView, thông báo), CsvExporter (xuất Excel/CSV)
```
