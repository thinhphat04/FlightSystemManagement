using Newtonsoft.Json;

namespace FlightSystemManagement.Entity
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public int UploadedBy { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public DocumentType DocumentType { get; set; }

        [JsonIgnore]
        public ICollection<FlightDocument> FlightDocuments { get; set; }
    }
}
