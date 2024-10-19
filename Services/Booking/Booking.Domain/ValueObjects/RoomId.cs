using Booking.Domain.Exceptions;

namespace Booking.Domain.ValueObjects;
public record RoomId
{
    public Guid Value { get; }
    private RoomId(Guid value) => Value = value;
    public static RoomId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("RoomId cannot be empty.");
        }

        return new RoomId(value);
    }
}
