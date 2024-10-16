namespace FlightSystemManagement.DTO
{
    public class DocumentCreateDto
    {
        public string Title { get; set; }
        public string Type { get; set; }

        public int CreatorId { get; set; } // User ID of the creator
   
        public string Note { get; set; }
    }
}