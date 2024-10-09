namespace FlightSystemManagement.Entity
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public int PermissionGroupID { get; set; } // Foreign key
        public string Role { get; set; }

        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDownload { get; set; }

        // Navigation properties
        public PermissionGroup PermissionGroup { get; set; } // Navigation property
    }
}