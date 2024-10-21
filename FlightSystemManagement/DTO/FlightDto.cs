namespace FlightSystemManagement.DTO;

public class FlightDto
{
    public int FlightID { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureDate { get; set; }
    public string PointOfLoading { get; set; }
    public string PointOfUnloading { get; set; }
    public bool IsFlightCompleted { get; set; }
    public List<FlightDocumentDto> FlightDocuments { get; set; }
}