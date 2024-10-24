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
         public async Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto, IFormFile file, ClaimsPrincipal user)
    {
    // Get the document by its ID
    var document = await _context.Documents.FindAsync(documentId);
    if (document == null) return null;

    // Check if the user has permission to update the document
    var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
    if (userRole != "Admin" && userRole != "Back-Office")
    {
        throw new UnauthorizedAccessException("You do not have permission to update this document.");
    }

    // Increment document version
    var currentVersionParts = document.Version?.Split('.') ?? new[] { "1", "0" };
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

    // Update other document properties
    document.Title = dto.Title;
    document.Type = dto.Type;
    document.Note = dto.Note;

    // Handle file updates if a new file is provided
    if (file != null && file.Length > 0)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

        // Ensure directory exists
        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));
        }

        // Delete the old file if it exists
        if (!string.IsNullOrEmpty(document.FilePath) && File.Exists(document.FilePath))
        {
            File.Delete(document.FilePath);
        }

        // Save the new file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Update the file path in the document
        document.FilePath = filePath;
    }

    // Save changes to the database
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