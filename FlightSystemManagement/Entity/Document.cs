namespace FlightSystemManagement.Entity
{
    public class Document
    {
        public int DocumentID { get; set; } // Unique identifier for the document
        public string DocumentName { get; set; } // Name of the document, auto-updated after file upload
        public int DocumentTypeID { get; set; } // Foreign key referencing DocumentType
        public int UploadedBy { get; set; } // The ID of the user who uploaded the document
        public DateTime CreatedDate { get; set; } // Date the document was created
        public string Version { get; set; } // Version of the document, e.g., 1.0, 1.1
        public string Note { get; set; } // Additional notes about the document

        // Navigation properties
        public DocumentType DocumentType { get; set; } // Navigation property to DocumentType entity
        public User User { get; set; } // Navigation property to User entity
        public ICollection<FlightDocument> FlightDocuments { get; set; } // Navigation property to FlightDocuments

        // Permissions
        public ICollection<PermissionGroupAssignment> PermissionGroupAssignments { get; set; } // Navigation property to Permission Groups
    }
}