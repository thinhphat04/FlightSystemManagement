using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlightSystemManagement.DTO.Flight;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FlightSystemManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // CREATE Flight
        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightCreateDto dto)
        {
            var flight = await _flightService.CreateFlightAsync(dto);
            if (flight == null)
            {
                return BadRequest("Failed to create flight.");
            }

            return Ok(flight);
        }

        // READ Flight by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // READ All Flights
        [HttpGet]
        public async Task<IActionResult> GetAllFlights([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var flights = await _flightService.GetAllFlightsAsync();
            var paginatedFlights = flights
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        
            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = flights.Count,
                Data = paginatedFlights
            });
        }

        // UPDATE Flight
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightCreateDto dto)
        {
            var updatedFlight = await _flightService.UpdateFlightAsync(id, dto);
            if (updatedFlight == null)
            {
                return NotFound("Flight not found.");
            }

            return Ok(updatedFlight);
        }

        // DELETE Flight
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlightAsync(id);
            if (!result)
            {
                return NotFound("Flight not found.");
            }

            return NoContent();
        }
        [Authorize(Roles = "Admin,Back-Office")]
        [HttpPost("{flightId}/documents/{documentId}")]
        public async Task<IActionResult> AddDocumentToFlight(int flightId, int documentId)
        {
            try
            {
                var result = await _flightService.AddDocumentToFlight(flightId, documentId);
                if (result)
                {
                    return Ok("Tài liệu đã được thêm vào chuyến bay thành công.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Không thể thêm tài liệu vào chuyến bay.");
        }
    }
}
