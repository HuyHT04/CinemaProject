USE CinemaBookingDB;
GO

-- USERS (3 role)
INSERT INTO AspNetUsers (Id, FullName, Email, PasswordHash, RoleName)
VALUES
('U001', 'Admin User', 'admin@cinema.com', '123456', 'Admin'),
('U002', 'Staff One', 'staff@cinema.com', '123456', 'Staff'),
('U003', 'Customer One', 'user@cinema.com', '123456', 'Customer');

-- MOVIES
INSERT INTO Movies (Title, Description, Genre, Duration, Price, PosterUrl)
VALUES
(N'Dune: Part Two', N'Sci-fi epic continues.', N'Sci-Fi', 165, 85000, '/img/dune2.jpg'),
(N'Inside Out 2', N'Animated emotions return.', N'Animation', 110, 70000, '/img/io2.jpg');

-- ROOMS
INSERT INTO Rooms (Name, Capacity) VALUES ('Room 1', 20), ('Room 2', 20);

-- SEATS: 4 hàng A-D, mỗi hàng 5 ghế
DECLARE @roomId INT = 1;
DECLARE @rows NVARCHAR(4) = 'ABCD';
DECLARE @i INT = 1;
WHILE @i <= LEN(@rows)
BEGIN
    DECLARE @r NVARCHAR(1) = SUBSTRING(@rows, @i, 1);
    DECLARE @n INT = 1;
    WHILE @n <= 5
    BEGIN
        INSERT INTO Seats (Code, IsAvailable, RoomId)
        VALUES (CONCAT(@r, @n), 1, @roomId);
        SET @n += 1;
    END
    SET @i += 1;
END;

-- SHOWTIMES
INSERT INTO Showtimes (MovieId, RoomId, StartTime)
VALUES
(1, 1, '2025-11-05 19:30'),
(2, 2, '2025-11-05 20:00');

-- BOOKINGS
INSERT INTO Bookings (UserId, ShowtimeId, SelectedSeats, TotalPrice, Status)
VALUES
('U003', 1, 'A1,A2', 170000, 'Paid');
