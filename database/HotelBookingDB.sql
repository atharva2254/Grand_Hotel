-- Schema + sample data for Rooms and Bookings.
-- The ASP.NET Identity tables (AspNetUsers, AspNetRoles, ...) and the admin/staff users are
-- created automatically by the app on first run (DbSeeder). To get a full script including
-- Identity tables, use SSMS: Database > Tasks > Generate Scripts, after running the app once.
IF DB_ID('HotelBookingDB') IS NULL CREATE DATABASE HotelBookingDB;
GO
USE HotelBookingDB;
GO
CREATE TABLE Rooms (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    RoomNumber     NVARCHAR(10)  NOT NULL,
    RoomType       NVARCHAR(50)  NOT NULL,
    Description    NVARCHAR(500) NULL,
    Capacity       INT           NOT NULL,
    PricePerNight  DECIMAL(10,2) NOT NULL,
    ImageUrl       NVARCHAR(300) NULL,
    IsActive       BIT           NOT NULL DEFAULT 1,
    CONSTRAINT CK_Room_Capacity CHECK (Capacity BETWEEN 1 AND 10)
);
CREATE UNIQUE INDEX IX_Rooms_RoomNumber ON Rooms(RoomNumber);

CREATE TABLE Bookings (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Reference     NVARCHAR(450) NOT NULL,
    RoomId        INT           NOT NULL,
    GuestName     NVARCHAR(100) NOT NULL,
    GuestEmail    NVARCHAR(150) NOT NULL,
    GuestPhone    NVARCHAR(20)  NOT NULL,
    CheckIn       DATE          NOT NULL,
    CheckOut      DATE          NOT NULL,
    Guests        INT           NOT NULL,
    TotalPrice    DECIMAL(10,2) NOT NULL,
    Status        NVARCHAR(20)  NOT NULL DEFAULT 'Confirmed',
    CreatedAtUtc  DATETIME2     NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Bookings_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id),
    CONSTRAINT CK_Booking_Dates CHECK (CheckOut > CheckIn)
);
CREATE UNIQUE INDEX IX_Bookings_Reference ON Bookings(Reference);
CREATE INDEX IX_Bookings_Room_Dates ON Bookings(RoomId, CheckIn, CheckOut);
GO
INSERT INTO Rooms (RoomNumber, RoomType, Description, Capacity, PricePerNight) VALUES
('101','Standard Twin','Two single beds, AC, free Wi-Fi.',2,2500),
('102','Standard Twin','Two single beds, AC, free Wi-Fi.',2,2500),
('201','Deluxe King','King bed, city view, minibar.',2,4000),
('202','Deluxe King','King bed plus sofa bed, balcony.',3,4500),
('301','Family Suite','Two bedrooms, living area, kitchenette.',4,7000);
INSERT INTO Bookings (Reference, RoomId, GuestName, GuestEmail, GuestPhone, CheckIn, CheckOut, Guests, TotalPrice, Status)
VALUES ('HBSAMPLE1',1,'Sample Guest','guest@example.com','9999999999',DATEADD(DAY,5,CAST(GETDATE() AS DATE)),DATEADD(DAY,7,CAST(GETDATE() AS DATE)),2,5000,'Confirmed');
GO
