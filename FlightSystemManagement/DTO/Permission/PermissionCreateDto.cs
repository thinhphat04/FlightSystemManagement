namespace FlightSystemManagement.DTO.Permission
{
    public class PermissionCreateDto
    {
        public int DocumentID { get; set; }  // The document to set permission for
        public int PermissionGroupID { get; set; }  // The group (Pilot, Crew)
        public bool CanView { get; set; }   // View permission
        public bool CanEdit { get; set; }   // Edit permission
        public bool CanDownload { get; set; }  // Download permission
    }
}