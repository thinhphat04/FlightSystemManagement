using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightSystemContext _context;

        public DocumentService(FlightSystemContext context)
        {
            _context = context;
        }

        // Create a new document
        public async Task<Document> CreateDocumentAsync(DocumentCreateDto dto, string filePath)
        {
            var document = new Document
            {
                Title = dto.Title,
                Type = dto.Type,
                CreatorID = dto.CreatorId,
                Note = dto.Note,
                FilePath = filePath  // Đường dẫn file PDF được upload
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }
        
        
        // Get a document by ID
        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await _context.Documents
                                 .Include(d => d.Creator)
                                 .FirstOrDefaultAsync(d => d.DocumentID == documentId);
        }

        // Get all documents
        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents
                                 .Include(d => d.Creator)
                                 .ToListAsync();
        }

        // Update a document
        public async Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return null;

            // Tách phiên bản hiện tại thành phần integer và decimal
            var currentVersionParts = document.Version.Split('.');

            if (currentVersionParts.Length == 2 &&
                int.TryParse(currentVersionParts[0], out int majorVersion) &&
                int.TryParse(currentVersionParts[1], out int minorVersion))
            {
                // Tăng phần số thập phân (minor version) lên 1
                minorVersion += 1;
                document.Version = $"{majorVersion}.{minorVersion}";
            }
            else
            {
                // Nếu không thể tách phiên bản, đặt phiên bản thành 1.1
                document.Version = "1.1";
            }

            // Cập nhật các thông tin khác của tài liệu
            document.Title = dto.Title;
            document.Type = dto.Type;
            document.Note = dto.Note;

            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }

        // Delete a document
        public async Task<bool> DeleteDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<Document> AddDocumentToFlightAsync(int flightId, DocumentCreateDto dto)
        {
            // Lấy chuyến bay từ database
            var flight = await _context.Flights
                .Include(f => f.FlightDocuments)
                .FirstOrDefaultAsync(f => f.FlightID == flightId);

            // Kiểm tra nếu chuyến bay không tồn tại hoặc đã hoàn tất
            if (flight == null || flight.IsFlightCompleted)
            {
                throw new InvalidOperationException("Chuyến bay không tồn tại hoặc đã hoàn tất, không thể thêm tài liệu.");
            }

            // Tạo tài liệu mới từ DTO
            var document = new Document
            {
                Title = dto.Title,
                Type = dto.Type,
                CreatorID = dto.CreatorId,
                Note = dto.Note,
            };

            // Thêm tài liệu vào hệ thống
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            // Tạo liên kết tài liệu với chuyến bay
            var flightDocument = new FlightDocument
            {
                FlightID = flight.FlightID,
                DocumentID = document.DocumentID,
                CreatedDate = DateTime.Now
            };

            _context.FlightDocuments.Add(flightDocument);
            await _context.SaveChangesAsync();

            return document;
        }

    }
}
