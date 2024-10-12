using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlightSystemManagement.Services.Interfaces;

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
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
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
    }
}
