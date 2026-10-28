using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Room>(e =>
        {
            e.HasIndex(r => r.RoomNumber).IsUnique();
            e.Property(r => r.PricePerNight).HasColumnType("decimal(10,2)");
            e.ToTable(t => t.HasCheckConstraint("CK_Room_Capacity", "[Capacity] BETWEEN 1 AND 10"));
        });

        b.Entity<Booking>(e =>
        {
            e.HasIndex(x => x.Reference).IsUnique();
            e.HasIndex(x => new { x.RoomId, x.CheckIn, x.CheckOut });
            e.Property(x => x.TotalPrice).HasColumnType("decimal(10,2)");
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            e.Ignore(x => x.Nights);
            e.HasOne(x => x.Room).WithMany(r => r.Bookings)
             .HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Restrict);
            e.ToTable(t => t.HasCheckConstraint("CK_Booking_Dates", "[CheckOut] > [CheckIn]"));
        });
    }
}
