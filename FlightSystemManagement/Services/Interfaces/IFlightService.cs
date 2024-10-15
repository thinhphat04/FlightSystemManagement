using FlightSystemManagement.Entity;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services.Interfaces
{
    public interface IFlightService
    {
        // CREATE Flight
        Task<Flight> CreateFlightAsync(FlightCreateDto dto);

        // READ Flight by ID
        Task<Flight> GetFlightByIdAsync(int flightId);

        // READ All Flights
        Task<IEnumerable<Flight>> GetAllFlightsAsync();

        // UPDATE Flight
        Task<Flight> UpdateFlightAsync(int flightId, FlightCreateDto dto);

        // DELETE Flight
        Task<bool> DeleteFlightAsync(int flightId);
        
        // Add document to flight
        Task<bool> AddDocumentToFlight(int flightId, int documentId);
    }
}