using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services;

public class AvailabilityService
{
    private readonly AppDbContext _db;
    public AvailabilityService(AppDbContext db) => _db = db;

    // A room is taken if any non-cancelled booking overlaps [checkIn, checkOut).
    // Check-out day is free for the next guest: existing.In < newOut && existing.Out > newIn.
    public IQueryable<Room> AvailableRooms(DateOnly checkIn, DateOnly checkOut, int guests) =>
        _db.Rooms.Where(r => r.IsActive && r.Capacity >= guests &&
            !r.Bookings.Any(b => b.Status != BookingStatus.Cancelled &&
                                 b.CheckIn < checkOut && b.CheckOut > checkIn));

    public Task<bool> IsAvailableAsync(int roomId, DateOnly checkIn, DateOnly checkOut, int guests) =>
        AvailableRooms(checkIn, checkOut, guests).AnyAsync(r => r.Id == roomId);

    public static string? ValidateStay(DateOnly? checkIn, DateOnly? checkOut, int guests)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        if (checkIn is null || checkOut is null) return "Please choose check-in and check-out dates.";
        if (checkIn < today) return "Check-in cannot be in the past.";
        if (checkOut <= checkIn) return "Check-out must be after check-in.";
        if (checkOut.Value.DayNumber - checkIn.Value.DayNumber > 30) return "Maximum stay is 30 nights.";
        if (guests < 1 || guests > 10) return "Guests must be between 1 and 10.";
        return null;
    }
}
