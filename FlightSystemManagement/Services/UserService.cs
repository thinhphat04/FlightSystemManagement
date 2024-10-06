using FlightSystemManagement.Data;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Services
{
    public class UserService : IUserService
    {
        private readonly FlightSystemContext _context;

        public UserService(FlightSystemContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            // Kiểm tra email thuộc domain VietjetAir
            if (!email.EndsWith("@vietjetair.com", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            // Kiểm tra mật khẩu
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            if (user == null) return null;

            // Load roles for the user
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserID == user.UserID)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();

            // Attach roles to user for further use
            user.UserRoles = userRoles.Select(roleName => new UserRole { UserID = user.UserID, Role = new Role { RoleName = roleName } }).ToList();

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).SingleOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Kiểm tra email thuộc domain VietjetAir
            if (!user.Email.EndsWith("@vietjetair.com", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Email must belong to the 'vietjetair.com' domain.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool IsAdmin(int userId)
        {
            return _context.UserRoles.Any(ur => ur.UserID == userId && ur.Role.RoleName == "Admin");
        }

        public bool IsBackOffice(int userId)
        {
            return _context.UserRoles.Any(ur => ur.UserID == userId && ur.Role.RoleName == "BackOffice");
        }

        public bool IsPilotOrCabinCrew(int userId)
        {
            return _context.UserRoles.Any(ur => ur.UserID == userId && 
                (ur.Role.RoleName == "Pilot" || ur.Role.RoleName == "CabinCrew"));
        }
    }
}
