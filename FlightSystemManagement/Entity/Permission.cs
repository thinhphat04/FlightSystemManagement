namespace FlightSystemManagement.Entity
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public int DocumentID { get; set; }
        public Document Document { get; set; }
    
        public int PermissionGroupID { get; set; }
        public PermissionGroup PermissionGroup { get; set; }
    
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDownload { get; set; }
        public bool NoPermission { get; set; }
    }

}