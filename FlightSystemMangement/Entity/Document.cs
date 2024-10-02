namespace FlightSystemMangement.Entity;

public class Document
{
    public int Id { get; set; } // Unique identifier for the document
    public string Title { get; set; } // Title of the document
    public string FilePath { get; set; } // File path of the document
    public int DocumentTypeId { get; set; } // Foreign key reference to DocumentType
    public DocumentType DocumentType { get; set; } // Navigation property
    public DateTime UploadedDate { get; set; } // Date the document was uploaded
    public int UploadedBy { get; set; } // Foreign key reference to User who uploaded the document
    public User User { get; set; } // Navigation property
    
    // Navigation properties
    public ICollection<FlightDocument> FlightDocuments { get; set; } // One-to-Many relationship with FlightDocuments
}
