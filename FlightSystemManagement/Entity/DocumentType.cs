namespace FlightSystemManagement.Entity
{
    public class DocumentType
    {
        public int DocumentTypeID { get; set; }
        public string TypeName { get; set; }

        // Navigation property
        public ICollection<Document> Documents { get; set; }
    }
}