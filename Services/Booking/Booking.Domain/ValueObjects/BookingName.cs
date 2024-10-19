namespace Booking.Domain.ValueObjects;
public record BookingName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private BookingName(string value) => Value = value;
    public static BookingName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new BookingName(value);
    }
}
