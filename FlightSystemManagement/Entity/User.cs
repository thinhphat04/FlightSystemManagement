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
       
        public string Status { get; set; } = "Active";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Role { get; set; }
        public string RefreshToken { get; set; } // Token làm mới
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        
     
    }
}