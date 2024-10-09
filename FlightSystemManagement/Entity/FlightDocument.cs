namespace FlightSystemManagement.Entity
{
    public class FlightDocument
    {
        public int FlightDocumentID { get; set; } // Primary Key
        public int FlightID { get; set; }
        public int DocumentID { get; set; }
        public DateTime CreatedDate { get; set; } // Thêm trường thời gian tạo tài liệu

        // Navigation properties
        public Flight Flight { get; set; }
        public Document Document { get; set; }
    }
}