using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (!await session.Query<Room>().AnyAsync())
            session.Store(GetPreconfiguredRooms());
        //if (!await session.Query<Hotel>().AnyAsync())
        //    session.Store(GetPreconfiguredHotels());
        if (!await session.Query<Service>().AnyAsync())
            session.Store(GetPreconfiguredServices());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Room> GetPreconfiguredRooms()
    {
        return
[
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.DeluxeSuite,
        Capacity = 2,
        Description = "A luxurious suite with stunning views and high-end amenities.",
        PricePerNight = 250.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-1.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Television", "Mini-bar" },
        Rating = 4.5M,
        Beds = 1,
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.StandardRoom,
        Capacity = 2,
        Description = "A comfortable room with essential amenities for a relaxing stay.",
        PricePerNight = 150.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-2.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Television" },
        Rating = 4.0M,
        Beds = 1
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.FamilySuite,
        Capacity = 4,
        Description = "A spacious suite perfect for families with multiple bedrooms and bathrooms.",
        PricePerNight = 300.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-3.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Television", "Kitchenette" },
        Rating = 4.8M,
        Beds = 2
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.SingleRoom,
        Capacity = 1,
        Description = "A cozy room for single travelers looking for comfort and value.",
        PricePerNight = 100.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-1.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Television" },
        Rating = 3.8M,
        Beds = 1
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.PresidentialSuite,
        Capacity = 2,
        Description = "The ultimate luxury experience with top-class amenities and services.",
        PricePerNight = 500.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-2.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Private pool", "Jacuzzi" },
        Rating = 5.0M,
        Beds = 1
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.BusinessSuite,
        Capacity = 2,
        Description = "Designed for business travelers with a work desk and essential facilities.",
        PricePerNight = 200.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-3.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Work desk", "Television" },
        Rating = 4.2M,
        Beds = 1,
    },
    new Room()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        RoomType = RoomTypes.PenthouseSuite,
        Capacity = 2,
        Description = "A premium suite with panoramic views and exclusive amenities.",
        PricePerNight = 600.00M,
        IsAvailable = true,
        ImageFiles = ["/images/rooms/room-1.jpg"],
        Amenities = new List<string>() { "Wi-Fi", "Air conditioning", "Private terrace", "Butler service" },
        Rating = 5.0M,
        Beds = 1
    }
];
    }

//    private static IEnumerable<Hotel> GetPreconfiguredHotels() => new List<Hotel>()
//{
//    new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Name = "Marmaris Beach Resort",
//        Address = "Atatürk Caddesi No:45",
//        City = "Marmaris",
//        Country = "Türkiye"
//    },
//    new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Name = "Kapadokya Cave Hotel",
//        Address = "Göreme Caddesi No:12",
//        City = "Nevşehir",
//        Country = "Türkiye"
//    },
//    new Hotel()
//    {
//        Id = Guid.NewGuid(),
//        Name = "İstanbul City Hotel",
//        Address = "İstiklal Caddesi No:89",
//        City = "İstanbul",
//        Country = "Türkiye"
//    }
//};
    private static IEnumerable<Service> GetPreconfiguredServices() => new List<Service>()
{
    new Service()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        Name = "Spa and Wellness",
        Description = "Relax and rejuvenate with our full-service spa and wellness center.",
        Price = 150.00M
    },
    new Service()
    {
        Id = Guid.NewGuid(),
       // HotelId = Guid.NewGuid(),
        Name = "Private Beach Access",
        Description = "Enjoy exclusive access to our private beach with full amenities.",
        Price = 100.00M
    },
    new Service()
    {
        Id = Guid.NewGuid(),
       // HotelId = Guid.NewGuid(),
        Name = "Guided Mountain Hikes",
        Description = "Experience the beauty of the mountains with guided hikes led by local experts.",
        Price = 75.00M
    },
    new Service()
    {
        Id = Guid.NewGuid(),
        //HotelId = Guid.NewGuid(),
        Name = "Ski Equipment Rental",
        Description = "Rent the latest ski equipment for your mountain adventures.",
        Price = 50.00M
    },
    new Service()
    {
        Id = Guid.NewGuid(),
   //     HotelId = Guid.NewGuid(),
        Name = "Gourmet Dining",
        Description = "Indulge in gourmet dining experiences with our world-class chefs.",
        Price = 200.00M
    },
    new Service()
    {
        Id = Guid.NewGuid(),
   //     HotelId = Guid.NewGuid(),
        Name = "City Tour Packages",
        Description = "Explore the city with our curated tour packages, featuring top attractions.",
        Price = 120.00M
    }
};
}
