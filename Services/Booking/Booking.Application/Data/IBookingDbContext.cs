using Microsoft.EntityFrameworkCore;
using Booking.Domain.Models;

namespace Booking.Application.Data
{//TODO Bu çok saçma repository pattern'i geri getir
    public interface IBookingDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        //public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<Room> Rooms { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
