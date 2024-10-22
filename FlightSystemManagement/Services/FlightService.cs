using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO.Document;
using FlightSystemManagement.DTO.Flight;
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

        public async Task<bool> AddDocumentToFlight(int flightId, int documentId)
        {
            // Kiểm tra chuyến bay có tồn tại và chưa kết thúc không
            var flight = await _context.Flights
                .Include(f => f.FlightDocuments)
                .FirstOrDefaultAsync(f => f.FlightID == flightId);

            if (flight == null)
            {
                // Trả về ngoại lệ nếu chuyến bay không tồn tại
                throw new Exception("Chuyến bay không tồn tại.");
            }

            if (flight.IsFlightCompleted)
            {
                // Trả về ngoại lệ nếu chuyến bay đã kết thúc
                throw new Exception("Không thể thêm tài liệu vào chuyến bay đã kết thúc.");
            }

            // Kiểm tra nếu tài liệu đã được thêm trước đó vào chuyến bay
            var existingDocument = flight.FlightDocuments
                .FirstOrDefault(fd => fd.DocumentID == documentId);

            if (existingDocument != null)
            {
                // Trả về ngoại lệ nếu tài liệu đã được thêm vào trước đó
                throw new Exception("Tài liệu đã tồn tại trong chuyến bay.");
            }

            // Thêm tài liệu vào chuyến bay
            var flightDocument = new FlightDocument
            {
                FlightID = flightId,
                DocumentID = documentId,
                CreatedDate = DateTime.Now
            };

            flight.FlightDocuments.Add(flightDocument);

            // Cập nhật totalDocuments
            flight.TotalDocuments = flight.FlightDocuments.Count;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return true;
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
                .Include(f => f.FlightDocuments) // Include FlightDocuments
                .ThenInclude(fd => fd.Document)  // Include Document for each FlightDocument
                .FirstOrDefaultAsync(f => f.FlightID == flightId);
        }

        // READ All Flights
     public async Task<List<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _context.Flights
            .Include(f => f.FlightDocuments)
                .ThenInclude(fd => fd.Document)
            .ToListAsync();

        // Ánh xạ từ Flight sang FlightDto
        var flightDtos = flights.Select(f => new FlightDto
        {
            FlightID = f.FlightID,
            FlightNumber = f.FlightNumber,
            DepartureDate = f.DepartureDate,
            PointOfLoading = f.PointOfLoading,
            PointOfUnloading = f.PointOfUnloading,
            IsFlightCompleted = f.IsFlightCompleted,
            FlightDocuments = f.FlightDocuments.Select(fd => new FlightDocumentDto
            {
                FlightDocumentID = fd.FlightDocumentID,
                DocumentID = fd.DocumentID,
                CreatedDate = fd.CreatedDate
            }).ToList()
        }).ToList();

        return flightDtos;
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
