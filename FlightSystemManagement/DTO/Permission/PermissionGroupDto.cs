namespace FlightSystemManagement.DTO.Permission
{
    
    public class PermissionGroupDto
    {
        public int FlightID { get; set; } // ID chuyến bay hoặc nhóm quyền khác
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDownload { get; set; }
    }

}


