using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Model;

namespace DAL.Data
{
    public class CinemaBookingRazorContext : DbContext
    {
        public CinemaBookingRazorContext(DbContextOptions<CinemaBookingRazorContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CinemaBookingDB;uid=sa;pwd=123;encrypt=true;trustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<Seat> Seats { get; set; } = default!;
        public DbSet<Showtime> Showtimes { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<AspNetUser> AspNetUsers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Seat>().ToTable("Seats");
            modelBuilder.Entity<Showtime>().ToTable("Showtimes");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<AspNetUser>().ToTable("AspNetUsers");
        }
    }
}
