namespace FlightSystemManagement.Entity
{
    public class UserFlightAssignment
    {
        public int AssignmentID { get; set; }
        public int UserID { get; set; }
        public int FlightID { get; set; }
        public string RoleOnFlight { get; set; }
        public DateTime AssignmentDate { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Flight Flight { get; set; }
    }
}