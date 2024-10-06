using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // POST: api/document
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreateDto documentDto)
        {
            if (documentDto == null)
            {
                return BadRequest("Document cannot be null");
            }

            var document = new Document
            {
                DocumentTypeID = documentDto.DocumentTypeID,
                FilePath = documentDto.FilePath,
                UploadedBy = documentDto.UploadedBy,
                UploadedDate = DateTime.Now
            };

            var createdDocument = await _documentService.CreateDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocument.DocumentID }, createdDocument);
        }

        // GET: api/document/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }
        
        // GET: api/document
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }
        
    }
}