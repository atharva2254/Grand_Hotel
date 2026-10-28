using System.Data;
using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using HotelBookingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers;

public class BookingController : Controller
{
    private readonly AppDbContext _db;
    private readonly AvailabilityService _avail;
    public BookingController(AppDbContext db, AvailabilityService avail) { _db = db; _avail = avail; }

    [HttpGet]
    public async Task<IActionResult> Create(int roomId, DateOnly checkIn, DateOnly checkOut, int guests)
    {
        var error = AvailabilityService.ValidateStay(checkIn, checkOut, guests);
        var room = await _db.Rooms.FindAsync(roomId);
        if (error != null || room == null || !await _avail.IsAvailableAsync(roomId, checkIn, checkOut, guests))
        {
            TempData["Error"] = error ?? "That room is no longer available for the selected dates.";
            return RedirectToAction("Index", "Home");
        }
        return View(new BookingVm { RoomId = roomId, CheckIn = checkIn, CheckOut = checkOut, Guests = guests, Room = room });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookingVm vm)
    {
        vm.Room = await _db.Rooms.FindAsync(vm.RoomId);
        var error = AvailabilityService.ValidateStay(vm.CheckIn, vm.CheckOut, vm.Guests);
        if (error != null) ModelState.AddModelError("", error);
        if (vm.Room == null || !ModelState.IsValid) return View(vm);

        // Serializable transaction prevents two guests booking the same room at once.
        await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        if (!await _avail.IsAvailableAsync(vm.RoomId, vm.CheckIn, vm.CheckOut, vm.Guests))
        {
            ModelState.AddModelError("", "Sorry, this room was just booked. Please search again.");
            return View(vm);
        }

        var booking = new Booking
        {
            Reference = "HB" + Guid.NewGuid().ToString("N")[..8].ToUpper(),
            RoomId = vm.RoomId, GuestName = vm.GuestName.Trim(), GuestEmail = vm.GuestEmail.Trim(),
            GuestPhone = vm.GuestPhone.Trim(), CheckIn = vm.CheckIn, CheckOut = vm.CheckOut,
            Guests = vm.Guests, TotalPrice = vm.Room.PricePerNight * vm.Nights
        };
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();
        await tx.CommitAsync();
        return RedirectToAction(nameof(Confirmation), new { reference = booking.Reference });
    }

    public async Task<IActionResult> Confirmation(string reference)
    {
        var b = await _db.Bookings.Include(x => x.Room).FirstOrDefaultAsync(x => x.Reference == reference);
        return b == null ? NotFound() : View(b);
    }

    [HttpGet]
    public IActionResult Lookup() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Lookup(LookupVm vm)
    {
        if (!ModelState.IsValid) return View();
        var reference = vm.Reference.Trim().ToUpper();
        var email = vm.Email.Trim();
        var b = await _db.Bookings.Include(x => x.Room)
            .FirstOrDefaultAsync(x => x.Reference == reference && x.GuestEmail == email);
        if (b == null) { ModelState.AddModelError("", "No booking found for that reference and email."); return View(); }
        ViewBag.Booking = b;
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(string reference, string email)
    {
        var b = await _db.Bookings.FirstOrDefaultAsync(x => x.Reference == reference && x.GuestEmail == email);
        if (b == null) return NotFound();
        if (b.Status == BookingStatus.Confirmed && b.CheckIn > DateOnly.FromDateTime(DateTime.Today))
        {
            b.Status = BookingStatus.Cancelled;
            await _db.SaveChangesAsync();
            TempData["Success"] = "Your booking has been cancelled.";
        }
        else TempData["Error"] = "This booking can no longer be cancelled online.";
        return RedirectToAction(nameof(Lookup));
    }
}
