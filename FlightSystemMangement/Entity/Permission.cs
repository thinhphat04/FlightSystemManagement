namespace FlightSystemMangement.Entity;

public class Permission
{
    public int Id { get; set; } // Unique identifier for the permission
    public int RoleId { get; set; } // Foreign key reference to Role
    public Role Role { get; set; } // Navigation property
    public int DocumentTypeId { get; set; } // Foreign key reference to DocumentType
    public DocumentType DocumentType { get; set; } // Navigation property
    public bool CanView { get; set; } = false; // Indicates if the role can view the document
    public bool CanCreate { get; set; } = false; // Indicates if the role can create documents
    public bool CanUpdate { get; set; } = false; // Indicates if the role can update documents
    public bool CanDelete { get; set; } = false; // Indicates if the role can delete documents
}
