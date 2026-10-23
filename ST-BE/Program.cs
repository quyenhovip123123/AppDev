using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ST_BE.Data;
using ST_BE.Services;
using System.Globalization;
using System.Security.Claims;
using System.Text;

// Thông báo lỗi luôn định dạng số kiểu Việt Nam (VD: 50.000đ) dù máy chủ cài ngôn ngữ nào
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("vi-VN");

var builder = WebApplication.CreateBuilder(args);

// ===== 1. Cấu hình JWT =====
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwt = jwtSection.Get<JwtSettings>() ?? throw new InvalidOperationException("Thiếu cấu hình JwtSettings");
if (Encoding.UTF8.GetByteCount(jwt.Secret) < 32)
    throw new InvalidOperationException("JwtSettings:Secret phải dài tối thiểu 32 byte cho HMAC-SHA256");
builder.Services.Configure<JwtSettings>(jwtSection);
builder.Services.AddScoped<TokenService>();

// ===== 2. Cơ sở dữ liệu SQL Server — chuỗi kết nối đọc từ appsettings.json, không viết cứng trong code =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection trong appsettings.json");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<AdoNetRepository>();   // Truy vấn ADO.NET thuần: mã vạch, thanh toán (SqlTransaction), Stored Procedure

// ===== CORS: cho phép client (WinForms / Swagger / web) gọi API =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("Token-Expired"));
});

// ===== 3. Xác thực bằng JWT Bearer =====
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },   // chặn tấn công đổi thuật toán (alg=none...)
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            // Sau khi chữ ký + hạn dùng hợp lệ: kiểm tra thêm tài khoản còn hoạt động và SecurityStamp còn khớp
            OnTokenValidated = async ctx =>
            {
                var db = ctx.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
                var userId = ctx.Principal!.GetUserId();
                var stamp = ctx.Principal!.FindFirstValue(AppClaims.SecurityStamp);
                var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null || !user.IsActive)
                    ctx.Fail("Tài khoản không tồn tại hoặc đã bị vô hiệu hóa");
                else if (user.SecurityStamp != stamp)
                    ctx.Fail("Phiên đăng nhập đã bị thu hồi");
            },
            // Trả lỗi 401 dạng JSON thống nhất để client hiển thị
            OnChallenge = async ctx =>
            {
                ctx.HandleResponse();
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var expired = ctx.AuthenticateFailure is SecurityTokenExpiredException;
                if (expired) ctx.Response.Headers.Append("Token-Expired", "true");
                await ctx.Response.WriteAsJsonAsync(new
                {
                    message = expired ? "Phiên đăng nhập đã hết hạn" :
                              ctx.AuthenticateFailure != null ? "Token không hợp lệ hoặc đã bị thu hồi" :
                              "Bạn cần đăng nhập để sử dụng chức năng này"
                });
            },
            OnForbidden = async ctx =>
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                await ctx.Response.WriteAsJsonAsync(new { message = "Bạn không có quyền thực hiện chức năng này" });
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// ===== 4. Swagger có nút "Authorize" để nhập JWT =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sunny Stationery Store API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Dán access token nhận được từ /api/auth/login (không cần gõ chữ Bearer)"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Áp dụng các EF Core Migration còn thiếu (tạo CSDL, bảng, Stored Procedure) rồi nạp dữ liệu mẫu.
// Tương đương chạy "Update-Database" / "dotnet ef database update".
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

// Lỗi không mong muốn -> trả JSON { message } thay vì trang lỗi HTML
app.UseExceptionHandler(errApp => errApp.Run(async ctx =>
{
    var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
    app.Logger.LogError(ex, "Unhandled exception");
    ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await ctx.Response.WriteAsJsonAsync(new { message = "Lỗi hệ thống, vui lòng thử lại sau" });
}));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Môi trường dev dùng được cả HTTP (http://localhost:5124) để test API / chạy client mà không cần tin cậy chứng chỉ
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseCors("AllowClient");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
