using Booking.Domain.Abstractions;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Models;
public class Room : Entity<RoomId>
{
    private Room()
    {
        
    }
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Room Create(RoomId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var room = new Room
        {
            Id = id,
            Name = name,
            Price = price
        };

        return room;
    }
}
