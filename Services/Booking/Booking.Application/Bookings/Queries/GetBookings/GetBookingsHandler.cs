using Booking.Application.Data;
using Booking.Application.Dtos;
using Booking.Application.Extensions;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Bookings.Queries.GetBookings;

internal class GetBookingsHandler(IBookingDbContext dbContext) : IQueryHandler<GetBookingsQuery, GetBookingsResult>
{
    public async Task<GetBookingsResult> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var count = await dbContext.Bookings.CountAsync(cancellationToken);
            var bookings = await dbContext.Bookings
                .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
                .Take(request.PaginationRequest.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var bookingsDto = bookings.Select(x => x.ConvertToBookingDto()).ToHashSet();

            return new GetBookingsResult(new PaginatedResult<BookingDto>(request.PaginationRequest.PageIndex, request.PaginationRequest.PageSize, count, bookingsDto));
        }
        catch (Exception)
        {

            throw;
        }

    }
}
