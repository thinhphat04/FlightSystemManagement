using FlightSystemManagement.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(Document document);
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int id);
        //get all document
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync();
    }
}