using System.Security.Claims;
using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.DTO.Document;
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
        public async Task<Document> CreateDocumentAsync(DocumentCreateDto dto, IFormFile file, ClaimsPrincipal user)
        {
            // Kiểm tra file
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Chỉ chấp nhận file PDF và DOCX
            var allowedExtensions = new[] { ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Only PDF and DOCX files are allowed.");
            }

            // Đường dẫn lưu file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));
            }

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Lấy CreatorId từ token Bearer
            var creatorIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (creatorIdClaim == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            // Gán CreatorId từ Bearer token thay vì từ phía client
            int creatorId = int.Parse(creatorIdClaim.Value);

            // Tạo tài liệu trong cơ sở dữ liệu
            var document = new Document
            {
                Title = dto.Title,
                Type = dto.Type,
                Note = dto.Note,
                CreatorID = creatorId,
                FilePath = filePath
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return document;
        }

        // Get a document by ID
        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await _context.Documents
                .Include(d => d.FlightDocuments) // Include FlightDocuments
                .ThenInclude(fd => fd.Flight)    // Include the Flight details if needed
                .FirstOrDefaultAsync(d => d.DocumentID == documentId);
        }

        // Get all documents
        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();  // No need to include Creator
        }

        // Update a document
        public async Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto, IFormFile file)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return null;

            // Increment document version
            var currentVersionParts = document.Version.Split('.');
            if (currentVersionParts.Length == 2 &&
                int.TryParse(currentVersionParts[0], out int majorVersion) &&
                int.TryParse(currentVersionParts[1], out int minorVersion))
            {
                minorVersion += 1;
                document.Version = $"{majorVersion}.{minorVersion}";
            }
            else
            {
                document.Version = "1.1";
            }

            // Update document properties
            document.Title = dto.Title;
            document.Type = dto.Type;
            document.Note = dto.Note;

            // Handle file updates
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));
                }
                if (!string.IsNullOrEmpty(document.FilePath) && File.Exists(document.FilePath))
                {
                    File.Delete(document.FilePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                document.FilePath = filePath;
            }

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

        // Add document to a flight
        public async Task<Document> AddDocumentToFlightAsync(int flightId, DocumentCreateDto dto)
        {
            var flight = await _context.Flights
                .Include(f => f.FlightDocuments)
                .FirstOrDefaultAsync(f => f.FlightID == flightId);

            if (flight == null || flight.IsFlightCompleted)
            {
                throw new InvalidOperationException("Flight does not exist or is already completed.");
            }

            var document = new Document
            {
                Title = dto.Title,
                Type = dto.Type,
                Note = dto.Note,
                CreatedDate = DateTime.Now
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

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