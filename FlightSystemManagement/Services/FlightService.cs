using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Services;

public class FlightService : IFlightService
{
    private readonly FlightSystemContext _context;

    public FlightService(FlightSystemContext context)
    {
        _context = context;
    }

    // Hàm để tạo mới một chuyến bay
    public async Task<Flight> CreateFlightAsync(FlightCreateDto flightDto)
    {
        var flight = new Flight
        {
            FlightNumber = flightDto.FlightNumber,
            Date = flightDto.Date,
            PointOfLoading = flightDto.PointOfLoading,
            PointOfUnloading = flightDto.PointOfUnloading
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    // Hàm để lấy thông tin chi tiết chuyến bay theo ID
    public async Task<Flight> GetFlightByIdAsync(int id)
    {
        return await _context.Flights.FindAsync(id);
    }

    // Hàm để lấy danh sách các chuyến bay
    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        return await _context.Flights.ToListAsync();
    }

    // Hàm để cập nhật chuyến bay
    public async Task<Flight> UpdateFlightAsync(Flight flight)
    {
        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();
        return flight;
    }

    // Hàm để xóa chuyến bay theo ID
    public async Task<bool> DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null)
        {
            return false;
        }

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> IsFlightCompletedAsync(int flightId)
    {
        var flight = await _context.Flights.FindAsync(flightId);
        return flight?.IsFlightCompleted ?? false; // Trả về true nếu chuyến bay đã kết thúc
    }
    
    public async Task<bool> AddDocumentToFlightAsync(int flightId, Document document)
    {
        // Tìm chuyến bay theo flightId
        var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightID == flightId);

        if (flight == null)
        {
            throw new Exception("Flight not found");
        }

        // Kiểm tra nếu chuyến bay đã kết thúc
        if (flight.IsFlightCompleted)
        {
            throw new Exception("Cannot add documents to a completed flight");
        }

        // Nếu chuyến bay chưa kết thúc, thêm tài liệu vào chuyến bay
        flight.FlightDocuments.Add(new FlightDocument
        {
            Document = document,
            Flight = flight
        });

        await _context.SaveChangesAsync();
        return true;
    }

}