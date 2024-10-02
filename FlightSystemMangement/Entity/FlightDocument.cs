namespace FlightSystemMangement.Entity;

public class FlightDocument
{
    public int Id { get; set; } // Unique identifier for the flight document
    public int FlightId { get; set; } // Foreign key reference to Flight
    public Flight Flight { get; set; } // Navigation property
    public int DocumentId { get; set; } // Foreign key reference to Document
    public Document Document { get; set; } // Navigation property
    public bool IsDownloaded { get; set; } = false; // Indicates if the document has been downloaded
    public DateTime? DownloadedDate { get; set; } // Date when the document was downloaded (nullable)
}
