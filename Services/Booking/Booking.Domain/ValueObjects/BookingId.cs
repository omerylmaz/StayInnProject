using Booking.Domain.Exceptions;

namespace Booking.Domain.ValueObjects;
public record BookingId
{
    public Guid Value { get; }
    private BookingId(Guid value) => Value = value;
    public static BookingId Of(Guid value)
    {
        //ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("BookingName cannot be empty.");
        }

        return new BookingId(value);
    }
}
