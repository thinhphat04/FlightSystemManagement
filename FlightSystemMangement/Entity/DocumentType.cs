namespace FlightSystemMangement.Entity;

public class DocumentType
{
    public int Id { get; set; } // Unique identifier for the document type
    public string TypeName { get; set; } // Name of the document type (e.g., Flight Manual, Safety Report)

    // Navigation properties
    public ICollection<Document> Documents { get; set; } // One-to-Many relationship with Documents
    public ICollection<Permission> Permissions { get; set; } // One-to-Many relationship with Permissions
}
