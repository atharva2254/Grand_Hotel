using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers;

[Authorize(Roles = "Staff,Admin")]
public class StaffBookingsController : Controller
{
    private readonly AppDbContext _db;
    public StaffBookingsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(BookingStatus? status, string? q)
    {
        var query = _db.Bookings.Include(b => b.Room).AsQueryable();
        if (status.HasValue) query = query.Where(b => b.Status == status);
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(b => b.Reference.Contains(q) || b.GuestName.Contains(q) || b.GuestEmail.Contains(q));
        ViewBag.Status = status; ViewBag.Q = q;
        return View(await query.OrderByDescending(b => b.CheckIn).ToListAsync());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetStatus(int id, BookingStatus status)
    {
        var b = await _db.Bookings.FindAsync(id);
        if (b == null) return NotFound();
        var ok = (b.Status, status) switch
        {
            (BookingStatus.Confirmed, BookingStatus.CheckedIn) => true,
            (BookingStatus.Confirmed, BookingStatus.Cancelled) => true,
            (BookingStatus.CheckedIn, BookingStatus.CheckedOut) => true,
            _ => false
        };
        if (ok) { b.Status = status; await _db.SaveChangesAsync(); TempData["Success"] = $"Booking {b.Reference} updated to {status}."; }
        else TempData["Error"] = "That status change is not allowed.";
        return RedirectToAction(nameof(Index));
    }
}
