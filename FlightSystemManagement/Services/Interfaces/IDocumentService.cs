using FlightSystemManagement.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(DocumentCreateDto dto, string filePath);
           Task<Document> GetDocumentByIdAsync(int documentId);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto);
        Task<bool> DeleteDocumentAsync(int documentId);
        
        Task<Document> AddDocumentToFlightAsync(int flightId, DocumentCreateDto dto);
    }
}