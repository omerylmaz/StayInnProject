using Booking.Application.Data;
using Booking.Application.Extensions;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Bookings.Queries.GetBookingsByRoom;

internal sealed class GetBookingsByNameHandler(IBookingDbContext dbContext) : IQueryHandler<GetBookingsByNameQuery, GetBookingsByRoomResult>
{
    public async Task<GetBookingsByRoomResult> Handle(GetBookingsByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var bookings = await dbContext.Bookings
                .AsNoTracking()
                .Where(x => x.BookingName == request.BookingName)
                .ToListAsync(cancellationToken);
            var bookingsDto = bookings.Select(x => x.ConvertToBookingDto()).ToHashSet();
            return new(bookingsDto);
        }
        catch (Exception)
        {
            throw;
        }

    }
}