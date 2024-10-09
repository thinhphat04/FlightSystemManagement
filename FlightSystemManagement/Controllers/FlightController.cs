using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystemManagement.Controllers;

 [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // API để tạo mới chuyến bay
        [HttpPost]
        public async Task<IActionResult> CreateFlight(FlightCreateDto flightDto)
        {
            var flight = await _flightService.CreateFlightAsync(flightDto);
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.FlightID }, flight);
        }
        

        // API để lấy thông tin chuyến bay theo ID
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

        // API để lấy danh sách các chuyến bay
        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        // API để cập nhật chuyến bay
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] Flight flight)
        {
            if (id != flight.FlightID)
            {
                return BadRequest("Flight ID mismatch");
            }

            var updatedFlight = await _flightService.UpdateFlightAsync(flight);
            return Ok(updatedFlight);
        }

        // API để xóa chuyến bay
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlightAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetFlightDetails(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            // Kiểm tra chuyến bay đã kết thúc chưa
            var isFlightCompleted = await _flightService.IsFlightCompletedAsync(id);
            return Ok(new { flight, isFlightCompleted });
        }
        
 


    }