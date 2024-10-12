namespace FlightSystemManagement.DTO;

public class FlightCreateDto
{
    public string FlightNumber { get; set; } // Flight number (e.g., VJ000)
    public DateTime DepartureDate { get; set; } // Date of the flight
    public string PointOfLoading { get; set; } // Departure airport (e.g., Noi Bai)
    public string PointOfUnloading { get; set; } // Arrival airport (e.g., Tan Son Nhat)
}