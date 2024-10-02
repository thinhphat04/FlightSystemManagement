namespace FlightSystemMangement.Entity;

public class UserRole
{
    public int UserId { get; set; } // Foreign key reference to User
    public User User { get; set; } // Navigation property
    public int RoleId { get; set; } // Foreign key reference to Role
    public Role Role { get; set; } // Navigation property
    public DateTime CreatedAt { get; set; } // Timestamp when the role was assigned
}
