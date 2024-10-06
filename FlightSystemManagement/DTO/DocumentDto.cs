namespace FlightSystemManagement.DTO
{
    public class DocumentDto
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; } // Add if you want to display the document type name
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UploadedBy { get; set; }
        public string UploadedByName { get; set; } // Add if you want to display the uploader's name
        public string DocumentName { get; set; }
        public string Note { get; set; }
    }
}