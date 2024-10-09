using AutoMapper;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using FlightSystemManagement.Services.Interfaces;

namespace FlightSystemManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentService documentService, IFlightService flightService, IMapper mapper)
        {
            _documentService = documentService;
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpPost("CreateDocument")]
        public async Task<IActionResult> CreateDocument(int flightId, [FromBody] DocumentCreateDto documentDto)
        {
            try
            {
                // Chuyển đổi DocumentCreateDto thành Document entity
                var document = new Document
                {
                    DocumentName = documentDto.DocumentName,
                    Version = documentDto.Version,
                    DocumentTypeID = documentDto.DocumentTypeID,
                    Note = documentDto.Note,
                   
                    
                };

                var createdDocument = await _documentService.CreateDocumentAsync(flightId, document, documentDto.PermissionGroupIds);
                return Ok(createdDocument);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, DocumentCreateDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            var updatedDocument = await _documentService.UpdateDocumentAsync(id, document);

            if (updatedDocument == null)
            {
                return NotFound();
            }

            return Ok(updatedDocument);
        }


    }
}