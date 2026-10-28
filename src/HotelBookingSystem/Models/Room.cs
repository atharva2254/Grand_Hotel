using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models;

public class Room
{
    public int Id { get; set; }

    [Required, StringLength(10), Display(Name = "Room number")]
    public string RoomNumber { get; set; } = "";

    [Required, StringLength(50), Display(Name = "Room type")]
    public string RoomType { get; set; } = "";

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(1, 10)]
    public int Capacity { get; set; } = 2;

    [Range(1, 1000000), Display(Name = "Price per night")]
    public decimal PricePerNight { get; set; }

    [StringLength(300), Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Active (bookable)")]
    public bool IsActive { get; set; } = true;

    public List<Booking> Bookings { get; set; } = new();
}
