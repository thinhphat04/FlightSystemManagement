namespace FlightSystemManagement.DTO.Document;

public class DocumentListDto
{
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public DateTime CreateDate { get; set; }
    public string Creator { get; set; }
    public string FlightNumber { get; set; }
}
