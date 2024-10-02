namespace FlightSystemMangement.Entity;

public class Flight
{
    public int Id { get; set; } // Unique identifier for the flight
    public string FlightNumber { get; set; } // Flight number
    public DateTime DepartureDate { get; set; } // Date and time of departure
    public DateTime ArrivalDate { get; set; } // Date and time of arrival
    public string FlightOrigin { get; set; } // Origin location of the flight
    public string FlightDestination { get; set; } // Destination location of the flight

    // Navigation properties
    public ICollection<FlightCrew> FlightCrews { get; set; } // Many-to-Many relationship with Users (FlightCrew)
    public ICollection<FlightDocument> FlightDocuments { get; set; } // One-to-Many relationship with FlightDocuments
}
