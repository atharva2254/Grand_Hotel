using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models;

public enum BookingStatus { Confirmed = 0, CheckedIn = 1, CheckedOut = 2, Cancelled = 3 }

public class Booking
{
    public int Id { get; set; }
    public string Reference { get; set; } = "";
    public int RoomId { get; set; }
    public Room? Room { get; set; }

    [Required, StringLength(100)] public string GuestName { get; set; } = "";
    [Required, EmailAddress, StringLength(150)] public string GuestEmail { get; set; } = "";
    [Required, Phone, StringLength(20)] public string GuestPhone { get; set; } = "";

    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int Guests { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public int Nights => CheckOut.DayNumber - CheckIn.DayNumber;
}
