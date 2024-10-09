using FlightSystemManagement.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(int flightId, Document document, List<int> permissionGroupIds);
        
        Task<Document> UpdateDocumentAsync(int documentId, Document updatedDocument);
    }
}