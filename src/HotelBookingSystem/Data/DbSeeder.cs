using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider sp)
    {
        var db = sp.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();

        var roles = sp.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var r in new[] { "Admin", "Staff" })
            if (!await roles.RoleExistsAsync(r)) await roles.CreateAsync(new IdentityRole(r));

        var users = sp.GetRequiredService<UserManager<IdentityUser>>();
        await EnsureUser(users, "admin@hotel.com", "Admin@123", "Admin");
        await EnsureUser(users, "staff@hotel.com", "Staff@123", "Staff");

        if (!await db.Rooms.AnyAsync())
        {
            db.Rooms.AddRange(
                new Room { RoomNumber = "101", RoomType = "Standard Twin", Capacity = 2, PricePerNight = 2500, Description = "Two single beds, AC, free Wi-Fi." },
                new Room { RoomNumber = "102", RoomType = "Standard Twin", Capacity = 2, PricePerNight = 2500, Description = "Two single beds, AC, free Wi-Fi." },
                new Room { RoomNumber = "201", RoomType = "Deluxe King", Capacity = 2, PricePerNight = 4000, Description = "King bed, city view, minibar." },
                new Room { RoomNumber = "202", RoomType = "Deluxe King", Capacity = 3, PricePerNight = 4500, Description = "King bed plus sofa bed, balcony." },
                new Room { RoomNumber = "301", RoomType = "Family Suite", Capacity = 4, PricePerNight = 7000, Description = "Two bedrooms, living area, kitchenette." });
            await db.SaveChangesAsync();
        }
    }

    private static async Task EnsureUser(UserManager<IdentityUser> um, string email, string pwd, string role)
    {
        if (await um.FindByEmailAsync(email) != null) return;
        var u = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
        var res = await um.CreateAsync(u, pwd);
        if (res.Succeeded) await um.AddToRoleAsync(u, role);
    }
}
