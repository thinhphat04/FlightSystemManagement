using FlightSystemManagement.DTO;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystemManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // Create a new document
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentCreateDto dto, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Chỉ chấp nhận file PDF
            if (!file.FileName.EndsWith(".pdf"))
            {
                return BadRequest("Only PDF files are allowed.");
            }

            // Đường dẫn lưu file PDF
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads"));
            }

            // Lưu file PDF vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Tạo tài liệu trong cơ sở dữ liệu
            var document = await _documentService.CreateDocumentAsync(dto, filePath);

            return CreatedAtAction(nameof(GetDocumentById), new { documentId = document.DocumentID }, document);
        }

      

        // Get a document by ID
        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetDocumentById(int documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);
            if (document == null)
                return NotFound();
            return Ok(document);
        }

        // Get all documents
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        // Update a document
        [HttpPut("{documentId}")]
        public async Task<IActionResult> UpdateDocument(int documentId, DocumentUpdateDto dto)
        {
            var document = await _documentService.UpdateDocumentAsync(documentId, dto);
            if (document == null)
                return NotFound();
            return Ok(document);
        }

        // Delete a document
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var success = await _documentService.DeleteDocumentAsync(documentId);
            if (!success)
                return NotFound();
            return NoContent();
        }
        
        [HttpPost("flight/{flightId}/add-document")]
        public async Task<IActionResult> AddDocumentToFlight(int flightId, [FromBody] DocumentCreateDto dto)
        {
            try
            {
                var document = await _documentService.AddDocumentToFlightAsync(flightId, dto);
                return CreatedAtAction(nameof(GetDocumentById), new { documentId = document.DocumentID }, document);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
