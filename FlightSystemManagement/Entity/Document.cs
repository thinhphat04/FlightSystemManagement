namespace FlightSystemManagement.Entity
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Version { get; set; } = "1.0"; // Default version
        public int CreatorID { get; set; }
        public string Note { get; set; }
        public string FilePath { get; set; } // For uploaded file
        public ICollection<Permission> Permissions { get; set; }
    
        // Add this navigation property to resolve the error
        public ICollection<FlightDocument> FlightDocuments { get; set; }  // Navigation property to FlightDocument
    }


}