using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CinemaBookingRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Keep existing AddDbContext (OnConfiguring in DbContext will provide connection)
            builder.Services.AddDbContext<CinemaBookingRazorContext>();

            // Cookie authentication (simple, works with claims we set on login)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<ISeatRepository, SeatRepository>();
            builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<ISeatService, SeatService>();
            builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication must be before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
