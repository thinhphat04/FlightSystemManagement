namespace FlightSystemManagement.DTO
{
    public class DocumentCreateDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string Version { get; set; } = "1.0"; // Default version 1.0
        public int CreatorId { get; set; } // User ID of the creator
        public string FilePath { get; set; } // File path where document is uploaded
        public string Note { get; set; }
    }
}