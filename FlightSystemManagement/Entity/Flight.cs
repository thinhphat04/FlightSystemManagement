namespace FlightSystemManagement.Entity
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = "Scheduled";

        // Navigation properties
        public ICollection<UserFlightAssignment> Assignments { get; set; }
        public ICollection<FlightDocument> FlightDocuments { get; set; }
    }
}