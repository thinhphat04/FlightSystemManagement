namespace FlightSystemMangement.Entity;

public class FlightCrew
{
    public int FlightId { get; set; } // Foreign key reference to Flight
    public Flight Flight { get; set; } // Navigation property
    public int UserId { get; set; } // Foreign key reference to User
    public User User { get; set; } // Navigation property
    public DateTime AssignedDate { get; set; } // Date when the user was assigned to the flight
}
