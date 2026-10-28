using System.ComponentModel.DataAnnotations;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.ViewModels;

public class SearchVm
{
    public DateOnly? CheckIn { get; set; }
    public DateOnly? CheckOut { get; set; }
    public int Guests { get; set; } = 2;
    public bool Searched { get; set; }
    public List<Room> Results { get; set; } = new();
}

public class BookingVm
{
    public int RoomId { get; set; }
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public int Guests { get; set; }

    [Required, StringLength(100), Display(Name = "Full name")]
    public string GuestName { get; set; } = "";
    [Required, EmailAddress, StringLength(150), Display(Name = "Email")]
    public string GuestEmail { get; set; } = "";
    [Required, Phone, StringLength(20), Display(Name = "Phone")]
    public string GuestPhone { get; set; } = "";

    public Room? Room { get; set; }
    public int Nights => CheckOut.DayNumber - CheckIn.DayNumber;
    public decimal Total => Room == null ? 0 : Room.PricePerNight * Nights;
}

public class LoginVm
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    public string? ReturnUrl { get; set; }
}

public class LookupVm
{
    [Required] public string Reference { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
}
