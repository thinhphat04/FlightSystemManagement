using FlightSystemManagement.Data;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightSystemContext _context;

        public DocumentService(FlightSystemContext context)
        {
            _context = context;
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return await _context.Documents.Include(d => d.DocumentType).Include(d => d.User).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.Include(d => d.DocumentType).Include(d => d.User)
                .SingleOrDefaultAsync(d => d.DocumentID == id);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _context.Documents
                .Select(doc => new DocumentDto
                {
                    DocumentID = doc.DocumentID,
                    DocumentTypeID = doc.DocumentTypeID,
                    FilePath = doc.FilePath,
                    UploadedDate = doc.UploadedDate,
                    UploadedBy = doc.UploadedBy,
                    DocumentName = doc.DocumentType.TypeName, // Assuming `TypeName` is the name
                    Note = "" // If you have a Note field in Document entity
                })
                .ToListAsync();

            return documents;
        }
    }
    
    
    
}