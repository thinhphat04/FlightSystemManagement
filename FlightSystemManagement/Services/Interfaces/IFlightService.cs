using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;

namespace FlightSystemManagement.Services.Interfaces;

public interface IFlightService
{
    //FlightCreateDto
    Task<Flight> CreateFlightAsync(FlightCreateDto flightCreateDto);
    Task<Flight> GetFlightByIdAsync(int id);
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task<Flight> UpdateFlightAsync(Flight flight);
    Task<bool> DeleteFlightAsync(int id);
    Task<bool> IsFlightCompletedAsync(int flightId);
    

}