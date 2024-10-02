namespace FlightSystemMangement.Entity;

public class User
{
    public int Id { get; set; } // Unique identifier for the user
    public string Email { get; set; } // Email address of the user
    public bool IsAdmin { get; set; } = false; // Indicates if the user is an admin
    public string Name { get; set; } // Name of the user
    public string PasswordHash { get; set; } // Encrypted password of the user
    public List<string> Wishlist { get; set; } // List of wishlist items for the user
    public string Phone { get; set; } // Phone number of the user
    public int? ResetPasswordOtp { get; set; } // OTP for password reset
    public DateTime? ResetPasswordOtpExpires { get; set; } // Expiry time for password reset OTP
    public List<string> Cart { get; set; } // List of products in the user's cart
    public string PaymentCustomerId { get; set; } // Payment customer ID for integration

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } // Many-to-Many relationship with Roles
    public ICollection<Document> Documents { get; set; } // One-to-Many relationship with Documents
    public ICollection<FlightCrew> FlightCrews { get; set; } // Many-to-Many relationship with Flights (FlightCrew)
}
