
namespace FlightSystemManagement.Entity
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<PermissionGroupAssignment> PermissionGroupAssignments { get; set; } // Các nhóm quyền được gán cho role này
    }
}