namespace FlightSystemManagement.Entity
{
    public class PermissionGroupAssignment
    {
        public int AssignmentID { get; set; }
        public int PermissionGroupID { get; set; } // Foreign key
        public int RoleID { get; set; } // Foreign key

        // Navigation properties
        public PermissionGroup PermissionGroup { get; set; }
        public Role Role { get; set; }
    }
}