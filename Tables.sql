CREATE DATABASE CinemaBookingDB;
GO
USE CinemaBookingDB;
GO

-- ========== USERS ==========
CREATE TABLE AspNetUsers (
    Id NVARCHAR(450) PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(200),
    RoleName NVARCHAR(50) NOT NULL
);
GO

-- ========== MOVIE ==========
CREATE TABLE Movies (
    Id INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(100),
    Description NVARCHAR(MAX),
    Genre NVARCHAR(50),
    Duration INT,
    Price DECIMAL(10,2),
    PosterUrl NVARCHAR(200)
);
GO

-- ========== ROOM ==========
CREATE TABLE Rooms (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    Capacity INT
);
GO

-- ========== SEAT ==========
CREATE TABLE Seats (
    Id INT IDENTITY PRIMARY KEY,
    Code NVARCHAR(10),
    IsAvailable BIT,
    RoomId INT FOREIGN KEY REFERENCES Rooms(Id)
);
GO

-- ========== SHOWTIME ==========
CREATE TABLE Showtimes (
    Id INT IDENTITY PRIMARY KEY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    RoomId INT FOREIGN KEY REFERENCES Rooms(Id),
    StartTime DATETIME
);
GO

-- ========== BOOKING ==========
CREATE TABLE Bookings (
    Id INT IDENTITY PRIMARY KEY,
    UserId NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id),
    ShowtimeId INT FOREIGN KEY REFERENCES Showtimes(Id),
    SelectedSeats NVARCHAR(100),
    TotalPrice DECIMAL(10,2),
    Status NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ========== PAYMENT (optional) ==========
CREATE TABLE Payments (
    Id INT IDENTITY PRIMARY KEY,
    BookingId INT FOREIGN KEY REFERENCES Bookings(Id),
    PaymentMethod NVARCHAR(50),
    PaymentTime DATETIME
);
GO
