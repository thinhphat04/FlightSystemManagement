namespace FlightSystemManagement.DTO.Permission;

public class PermissionDto
{
    public int PermissionID { get; set; }
    public int DocumentID { get; set; }
    public int PermissionGroupID { get; set; }
    public bool CanView { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDownload { get; set; }
}