namespace FlightSystemManagement.Entity
{
    public class PermissionGroup
    {
        public int PermissionGroupID { get; set; }
        public string GroupName { get; set; } // Pilot, Crew
        public ICollection<Permission> Permissions { get; set; }
    }

}