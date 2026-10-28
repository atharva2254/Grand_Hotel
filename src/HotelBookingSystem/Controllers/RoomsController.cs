using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers;

[Authorize(Roles = "Admin")]
public class RoomsController : Controller
{
    private readonly AppDbContext _db;
    public RoomsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.Rooms.OrderBy(r => r.RoomNumber).ToListAsync());

    public IActionResult Create() => View(new Room());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room room)
    {
        ModelState.Remove(nameof(Room.Bookings));
        if (await _db.Rooms.AnyAsync(r => r.RoomNumber == room.RoomNumber))
            ModelState.AddModelError(nameof(Room.RoomNumber), "Room number already exists.");
        if (!ModelState.IsValid) return View(room);
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Room created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var room = await _db.Rooms.FindAsync(id);
        return room == null ? NotFound() : View(room);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Room room)
    {
        if (id != room.Id) return BadRequest();
        if (await _db.Rooms.AnyAsync(r => r.RoomNumber == room.RoomNumber && r.Id != id))
            ModelState.AddModelError(nameof(Room.RoomNumber), "Room number already exists.");
        if (!ModelState.IsValid) return View(room);
        _db.Update(room);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Room updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var room = await _db.Rooms.FindAsync(id);
        return room == null ? NotFound() : View(room);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var room = await _db.Rooms.FindAsync(id);
        if (room == null) return NotFound();
        if (await _db.Bookings.AnyAsync(b => b.RoomId == id))
        {
            TempData["Error"] = "This room has bookings and cannot be deleted. Untick 'Active' in Edit to hide it instead.";
            return RedirectToAction(nameof(Index));
        }
        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Room deleted.";
        return RedirectToAction(nameof(Index));
    }
}
