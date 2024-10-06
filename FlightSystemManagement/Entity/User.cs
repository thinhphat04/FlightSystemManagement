namespace FlightSystemManagement.Entity
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<UserFlightAssignment> Assignments { get; set; }
        
        public User()
        {
            UserRoles = new List<UserRole>();
            Documents = new List<Document>();
            Assignments = new List<UserFlightAssignment>();
        }
    }
}