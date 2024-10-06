using FlightSystemManagement.Entity;


namespace FlightSystemManagement.Services.Interfaces
{
    public interface IUserService
    {
        // Đăng nhập người dùng
        Task<User> AuthenticateAsync(string email, string password);

        // Lấy danh sách tất cả người dùng
        Task<IEnumerable<User>> GetUsersAsync();

        // Lấy thông tin người dùng theo ID
        Task<User> GetUserByIdAsync(int id);

        // Tạo người dùng mới
        Task<User> CreateUserAsync(User user);

        // Xóa người dùng
        Task<bool> DeleteUserAsync(int id);
        
        // Kiểm tra người dùng có phải Admin hay không
        bool IsAdmin(int userId);

        // Kiểm tra người dùng có thuộc nhóm Back-Office hay không
        bool IsBackOffice(int userId);

        // Kiểm tra người dùng có phải là Phi công/Tiếp viên hay không
        bool IsPilotOrCabinCrew(int userId);
    }
}