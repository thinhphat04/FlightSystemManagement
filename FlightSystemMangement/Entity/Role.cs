namespace FlightSystemMangement.Entity;

public class Role
{
    public int Id { get; set; } // Unique identifier for the role
    public string RoleName { get; set; } // Name of the role (e.g., Admin, Pilot, Cabin Crew)
    public string Description { get; set; } // Description of the role

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } // Many-to-Many relationship with Users
    public ICollection<Permission> Permissions { get; set; } // One-to-Many relationship with Permissions
}
