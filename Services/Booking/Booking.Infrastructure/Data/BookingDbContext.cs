using Microsoft.EntityFrameworkCore;
using Booking.Application.Data;
using Booking.Domain.Models;
using System.Reflection;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Infrastructure.Data
{
    public class BookingDbContext(DbContextOptions options) : DbContext(options), IBookingDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        //public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
