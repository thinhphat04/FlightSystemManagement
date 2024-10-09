namespace FlightSystemManagement.DTO
{
    public class DocumentCreateDto
    {
        public string DocumentName { get; set; } // Name of the document
        public int DocumentTypeID { get; set; } // Document type ID
        public string Version { get; set; } = "1.0"; // Default version for new documents
        public string Note { get; set; } // Additional note for the document
        public List<int> PermissionGroupIds { get; set; } // List of permission group IDs for this document
        
        // Add if you want to display the uploader's name
        
    }
}