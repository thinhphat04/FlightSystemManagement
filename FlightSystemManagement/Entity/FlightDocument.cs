using System.Text.Json.Serialization;

namespace FlightSystemManagement.Entity
{
    public class FlightDocument
    {
        public int FlightDocumentID { get; set; }
    
        public int FlightID { get; set; }
        [JsonIgnore]
        public Flight Flight { get; set; }
    
        public int DocumentID { get; set; }
        public Document Document { get; set; }
    
        public DateTime CreatedDate { get; set; }
    }




}