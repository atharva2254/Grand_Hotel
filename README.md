Hotel Room Booking System

A web-based hotel booking application built with ASP.NET Core MVC, C#, and SQL Server. Guests can browse rooms, check availability, and submit booking requests, while staff can log in and manage room inventory.

Tech Stack

- ASP.NET Core MVC (.NET 10)
- C#
- SQL Server
- Entity Framework Core
- Razor Views, HTML, CSS, Bootstrap
- Cookie Authentication

Features

- Browse rooms and view pricing and details.
- Search rooms by dates and guest capacity.
- Book rooms with guest details and booking confirmation.
- Validate dates and check overlapping reservations.
- Staff login with password hashing.
- Staff-only room listing and room creation.

Project Structure

HotelBookingSystem/
├── src/
├── database/
│   ├── HotelBookingDB.bak
│   ├── HotelBookingDB.sql
│   └── ER-Diagram.png
├── screenshots/
├── AI_TOOLS_USED.md
└── README.md

Setup

1. Open "src/HotelBookingSystem.sln" in Visual Studio.
2. Configure the SQL Server connection string in "appsettings.json".
3. Restore NuGet packages.
4. Create/update the database using EF Core migrations or the supplied SQL script.
5. Run the "HotelBookingSystem.Web" project.

Design Decisions

- Used MVC to separate controllers, models, and views.
- Used EF Core for SQL Server database operations.
- Added server-side validation for booking dates and room availability.
- Used cookie authentication to protect staff-only pages.

Future Improvements

- Staff booking management and room editing.
- Guest accounts and booking history.
- Automated tests and stronger concurrency protection.

AI Tools

ChatGPT and Claude were used for development guidance, code generation, and troubleshooting. Refer to "AI_TOOLS_USED.md" for details.

Author
Atharva Lohar
