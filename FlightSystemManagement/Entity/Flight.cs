namespace FlightSystemManagement.Entity
{
    public class Flight
    {
        public int FlightID { get; set; } // Primary key
        public string FlightNumber { get; set; } // Flight number
        public DateTime DepartureDate { get; set; } // Flight date
        public string PointOfLoading { get; set; } // Departure location
        public string PointOfUnloading { get; set; } // Arrival location
        public bool IsFlightCompleted { get; set; } // Flight completion status
        
        public int TotalDocuments { get; set; }


        // Navigation properties
        public ICollection<FlightDocument> FlightDocuments { get; set; }
    }
}