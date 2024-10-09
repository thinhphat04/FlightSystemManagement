namespace FlightSystemManagement.Entity
{
    public class PermissionGroup
    {
        public int PermissionGroupID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<PermissionGroupAssignment> PermissionGroupAssignments { get; set; }
    }
}