using System.Text;
using FlightSystemManagement.Data;
using FlightSystemManagement.Services;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container
// Hỗ trợ Swagger/OpenAPI cho tài liệu hóa và kiểm thử API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt auth header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Sử dụng ReferenceHandler.Preserve
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//     options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
// });

// Đăng ký dịch vụ Controller để hỗ trợ API
builder.Services.AddControllers();

// Đăng ký dịch vụ IUserService với triển khai UserService, sử dụng Dependency Injection (DI)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
// Cấu hình xác thực JWT (JSON Web Token) cho ứng dụng
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Thiết lập các tùy chọn xác thực JWT
            ValidateIssuer = true, // Kiểm tra Issuer (người phát hành token)
            ValidateAudience = true, // Kiểm tra Audience (đối tượng nhận token)
            ValidateLifetime = true, // Kiểm tra thời hạn token
            ValidateIssuerSigningKey = true, // Kiểm tra khóa ký token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer hợp lệ, lấy từ cấu hình
            ValidAudience = builder.Configuration["Jwt:Issuer"], // Audience hợp lệ, lấy từ cấu hình
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Khóa ký token, lấy từ cấu hình
        };
    });

// Cấu hình DbContext cho ứng dụng, sử dụng Entity Framework Core với SQL Server
builder.Services.AddDbContext<FlightSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))); // Lấy chuỗi kết nối từ appsettings.json

var app = builder.Build();




// Thiết lập HTTP request pipeline (các bước xử lý yêu cầu HTTP)
if (app.Environment.IsDevelopment())
{
    // Nếu đang ở môi trường phát triển, sử dụng Swagger để kiểm thử và tài liệu hóa API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Chuyển hướng tất cả các yêu cầu HTTP sang HTTPS
app.UseHttpsRedirection();

// Bật tính năng xác thực và phân quyền
app.UseAuthentication(); // Kiểm tra xác thực bằng JWT
app.UseAuthorization(); // Kiểm tra quyền truy cập

// Định tuyến yêu cầu đến các controller
app.MapControllers();

// Chạy ứng dụng
app.Run();
