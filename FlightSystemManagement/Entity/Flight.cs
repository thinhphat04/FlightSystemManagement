namespace FlightSystemManagement.Entity
{
    public class Flight
    {
        public int FlightID { get; set; } // Unique identifier for the flight
        public string FlightNumber { get; set; } // Flight number (e.g., VJ000)
        public DateTime Date { get; set; } // Date of the flight
        public string PointOfLoading { get; set; } // Departure airport (e.g., Noi Bai)
        public string PointOfUnloading { get; set; } // Arrival airport (e.g., Tan Son Nhat)

        public bool IsFlightCompleted { get; set; } // True nếu chuyến bay đã kết thúc , False nếu chưa kết thúc
        // Navigation properties
        public ICollection<FlightDocument> FlightDocuments { get; set; } // Documents related to this flight
    }
    
}