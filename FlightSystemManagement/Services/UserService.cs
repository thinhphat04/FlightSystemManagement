using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlightSystemManagement.Data;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FlightSystemManagement.Services
{
    public class UserService : IUserService
{
    private readonly FlightSystemContext _context;
    private readonly IConfiguration _configuration;

    public UserService(FlightSystemContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    // xử lý đăng nhập
    public async Task<User> AuthenticateAsync(string email, string password)
    {
        if (!email.EndsWith("@vietjetair.com", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;

        return user;
    }
    // Tạo token
    public async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role) // Use role directly
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    // Lấy danh sách người dùng
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    // Lấy thông tin người dùng theo ID
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.UserID == id);
    }
    // Tạo người dùng mới
    public async Task<User> CreateUserAsync(User user)
    {
        // Kiểm tra nếu email không thuộc miền vietjetair.com
        if (!user.Email.EndsWith("@vietjetair.com", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Email must belong to the 'vietjetair.com' domain.");
        }

        // Hash the password before saving
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    // Xóa người dùng
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    // Role Checks
    public bool IsAdmin(User user)
    {
        return user.Role == "Admin";
    }

    public bool IsPilot(User user)
    {
        return user.Role == "Pilot";
    }

    public bool IsCrew(User user)
    {
        return user.Role == "Crew";
    }
    // Lưu Refresh Token
    public async Task SaveRefreshTokenAsync(int userId, string refreshToken)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
        }
    }
    // Lấy Refresh Token
    public async Task<string> GetRefreshTokenAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.RefreshToken;
    }
}

}
