namespace FlightSystemManagement.DTO
{
    public class DocumentCreateDto
    {
        public int DocumentTypeID { get; set; }
        public string FilePath { get; set; }
        public int UploadedBy { get; set; }
        public string DocumentName { get; set; } // Optional: Add fields as required
        public string Note { get; set; } // Optional: Add fields as required
    }
}