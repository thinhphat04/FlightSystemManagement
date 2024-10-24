using System.Security.Claims;
using FlightSystemManagement.DTO;
using FlightSystemManagement.DTO.Document;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Back-Office")] // Chỉ Admin và Nhân viên Back-Office được phép tạo tài liệu
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentCreateDto dto, IFormFile file)
        {
            try
            {
                var document = await _documentService.CreateDocumentAsync(dto, file, HttpContext.User);
                return CreatedAtAction(nameof(GetDocumentById), new { documentId = document.DocumentID }, document);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây nếu cần
                return StatusCode(500, new { message = "An error occurred while creating the document.", details = ex.Message });
            }
        }

        // Get a document by ID
        [Authorize(Roles = "Admin,Pilot,Crew")] // Admin, Phi công, và Tiếp viên đều có thể xem tài liệu
        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetDocumentById(int documentId)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(documentId);
                if (document == null)
                    return NotFound();
                return Ok(document);
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây nếu cần
                return StatusCode(500, new { message = "An error occurred while retrieving the document.", details = ex.Message });
            }
        }

        // Get all documents
        [Authorize(Roles = "Admin,Pilot,Crew")] // Admin, Phi công, và Tiếp viên đều có thể xem tất cả tài liệu
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _documentService.GetAllDocumentsAsync();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây nếu cần
                return StatusCode(500, new { message = "An error occurred while retrieving the documents.", details = ex.Message });
            }
        }

        // Update a document
        [Authorize(Roles = "Admin,Back-Office")] // Only Admin and Back-Office can update
        [HttpPut("{documentId}")]
        public async Task<IActionResult> UpdateDocument(int documentId, [FromForm] DocumentUpdateDto dto, IFormFile file)
        {
            try
            {
                // Call the service to update the document
                var updatedDocument = await _documentService.UpdateDocumentAsync(documentId, dto, file, User);

                if (updatedDocument == null)
                {
                    return NotFound("Document not found");
                }

                return Ok(updatedDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete a document
        [Authorize(Roles = "Admin")] // Chỉ Admin được phép xóa tài liệu
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                var success = await _documentService.DeleteDocumentAsync(documentId);
                if (!success)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây nếu cần
                return StatusCode(500, new { message = "An error occurred while deleting the document.", details = ex.Message });
            }
        }

        // Add document to a flight
        // [Authorize(Roles = "Admin,Back-Office")] // Chỉ Admin và Nhân viên Back-Office được phép thêm tài liệu vào chuyến bay
        // [HttpPost("flight/{flightId}/add-document")]
        // public async Task<IActionResult> AddDocumentToFlight(int flightId, [FromBody] DocumentCreateDto dto)
        // {
        //     try
        //     {
        //         var document = await _documentService.AddDocumentToFlightAsync(flightId, dto);
        //         return CreatedAtAction(nameof(GetDocumentById), new { documentId = document.DocumentID }, document);
        //     }
        //     catch (InvalidOperationException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log lỗi tại đây nếu cần
        //         return StatusCode(500, new { message = "An error occurred while adding the document to the flight.", details = ex.Message });
        //     }
        // }
    }
}