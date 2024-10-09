using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace FlightSystemManagement.Entity
{
    public class User 
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // điều này sẽ tạo một bảng trung gian UserRoles
        // nên bỏ [JsonIgnore] ở đây để tránh lỗi khi trả về dữ liệu
        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }
        
        public ICollection<Document> Documents { get; set; }


        
        public User()
        {
            UserRoles = new List<UserRole>();
            Documents = new List<Document>();
   
        }
    }
}