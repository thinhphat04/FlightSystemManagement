using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.Services.Interfaces;

namespace FlightSystemManagement.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightSystemContext _context;

        public FlightService(FlightSystemContext context)
        {
            _context = context;
        }

        // CREATE Flight
        public async Task<Flight> CreateFlightAsync(FlightCreateDto dto)
        {
            var flight = new Flight
            {
                FlightNumber = dto.FlightNumber,
                DepartureDate = dto.DepartureDate,
                PointOfLoading = dto.PointOfLoading,
                PointOfUnloading = dto.PointOfUnloading,
                IsFlightCompleted = false,
                TotalDocuments = 0
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        // READ Flight by ID
        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            return await _context.Flights
                .Include(f => f.FlightDocuments)
                .FirstOrDefaultAsync(f => f.FlightID == flightId);
        }

        // READ All Flights
        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights
                .Include(f => f.FlightDocuments)
                .ToListAsync();
        }

        // UPDATE Flight
        public async Task<Flight> UpdateFlightAsync(int flightId, FlightCreateDto dto)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightID == flightId);
            if (flight == null) return null;

            flight.FlightNumber = dto.FlightNumber;
            flight.DepartureDate = dto.DepartureDate;
            flight.PointOfLoading = dto.PointOfLoading;
            flight.PointOfUnloading = dto.PointOfUnloading;

            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        // DELETE Flight
        public async Task<bool> DeleteFlightAsync(int flightId)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightID == flightId);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task AddDocumentToFlight(int flightId, DocumentCreateDto documentDto)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightID == flightId);
            if (flight != null && !flight.IsFlightCompleted)
            {
                var document = new Document
                {
                    Title = documentDto.Title,
                   
                };

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                // Cập nhật số lượng tài liệu
                flight.TotalDocuments += 1;
                _context.Flights.Update(flight);
                await _context.SaveChangesAsync();
            }
        }

    }
}
