# Hotel Booking System

ASP.NET Core 10 MVC + Entity Framework Core + SQL Server. Guests search and book rooms; staff manage bookings; admins manage rooms.

## Tech stack
C#, ASP.NET Core 10 MVC (Razor), EF Core 10 (SQL Server), ASP.NET Core Identity (roles), Bootstrap 5.

## Setup
1. Install .NET 10 SDK and SQL Server (LocalDB works).
2. Edit the connection string in `src/HotelBookingSystem/appsettings.json` if you are not using LocalDB.
3. `cd src/HotelBookingSystem && dotnet run`
4. On first run the app creates the database, roles, two users and 5 sample rooms.
   (Alternative: run `database/HotelBookingDB.sql` for Rooms/Bookings only; Identity tables come from the app.)

## Test logins
| Role | Email | Password |
|---|---|---|
| Admin | admin@hotel.com | Admin@123 |
| Staff | staff@hotel.com | Staff@123 |

Guests need no account. They get a booking reference and can look up or cancel with reference + email.

## Design decisions
- **Availability rule:** a room is free if no non-cancelled booking overlaps the dates; check-out day is free for the next guest. Capacity must be >= guests.
- **No double booking:** booking is re-checked inside a Serializable transaction. DB also has check constraints (CheckOut > CheckIn, capacity 1-10) and unique room numbers/references.
- **Roles:** Staff can view/filter bookings and check in, check out, or cancel. Admin can do that plus create/edit/delete rooms.
- **Rooms with bookings cannot be deleted** (FK Restrict); mark them inactive instead.
- **Rules:** no past check-in, max 30 nights, 1-10 guests.
- **More with time:** payments, email confirmations, room photos upload, calendar view, seasonal pricing, migrations instead of EnsureCreated, unit tests.

## Features
Done: search by dates/guests, guest booking, booking lookup/cancel, staff login, booking list with filters and status changes, admin room CRUD, responsive UI.
Not done: payments, emails, image uploads, EF migrations.

## Known issues
Not yet compiled or tested by the author of this scaffold; run it and fix any build errors. Passwords above are for demo only.
