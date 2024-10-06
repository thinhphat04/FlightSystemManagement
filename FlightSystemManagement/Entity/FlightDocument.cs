namespace FlightSystemManagement.Entity
{
    public class FlightDocument
    {
        public int FlightDocumentID { get; set; }
        public int FlightID { get; set; }
        public int DocumentID { get; set; }
        public bool IsDownloaded { get; set; } = false;
        public DateTime? DownloadedDate { get; set; }

        // Navigation properties
        public Flight Flight { get; set; }
        public Document Document { get; set; }
    }
}