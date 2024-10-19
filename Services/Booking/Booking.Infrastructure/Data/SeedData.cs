using Microsoft.EntityFrameworkCore;
using Booking.Domain.Models;
using Booking.Domain.ValueObjects;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Infrastructure.Data
{
    internal class SeedData
    {
        public static async Task AddSeededDatas(BookingDbContext dbContext)
        {
            if (!await dbContext.Customers.AnyAsync())
            {
                await dbContext.Customers.AddRangeAsync(
                    Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "John Doe", "john.doe@gmail.com"),
                    Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "Jane Smith", "jane.smith@gmail.com"));
            }

            if (!await dbContext.Rooms.AnyAsync())
            {
                await dbContext.Rooms.AddRangeAsync(
                    Room.Create(RoomId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "Deluxe Room", 150),
                    Room.Create(RoomId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Standard Room", 100),
                    Room.Create(RoomId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Suite Room", 250),
                    Room.Create(RoomId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Single Room", 80));
            }

            if (!await dbContext.Bookings.AnyAsync())
            {
                var address1 = Address.Of("John", "Doe", "john.doe@gmail.com", "123 Main St", "USA", "New York", "10001");
                var address2 = Address.Of("Jane", "Smith", "jane.smith@gmail.com", "456 Elm St", "USA", "Los Angeles", "90001");

                var payment1 = Payment.Of("John Doe", "4111111111111111", "11/24", "123", 1);
                var payment2 = Payment.Of("Jane Smith", "4222222222222222", "12/25", "456", 2);

                var booking1 = BookingModel.Create(
                                    BookingId.Of(Guid.NewGuid()),
                                    CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                                    "RES_1",
                                    address1,
                                    payment1,
                                    RoomId.Of(Guid.NewGuid()),
                                    3,
                                    DateTime.Now
                                    );
                //booking1.AddBookingItem(RoomId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 3, 150);
                //booking1.AddBookingItem(RoomId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 2, 100);

                var booking2 = BookingModel.Create(
                                    BookingId.Of(Guid.NewGuid()),
                                    CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                                    "RES_2",
                                    billingAddress: address2,
                                    payment: payment2,
                                    RoomId.Of(Guid.NewGuid()),
                                    3,
                                    DateTime.Now
                                    );
                //booking2.AddBookingItem(RoomId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 250);
                //booking2.AddBookingItem(RoomId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 1, 80);

                await dbContext.Bookings.AddRangeAsync(booking1, booking2);
            }
            await dbContext.SaveChangesAsync();
        }
    }

}
