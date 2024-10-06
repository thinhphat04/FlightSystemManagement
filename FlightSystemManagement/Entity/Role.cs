using FlightSystemManagement.Entity;

namespace FlightSystemManagement.Entity
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; }
    }
}