using BuildingBlocks.Exceptions;

namespace Booking.Application.Exceptions;
public class BookingNotFoundException : NotFoundException
{
    public BookingNotFoundException(Guid id) : base("Booking", id)
    {
    }
}
