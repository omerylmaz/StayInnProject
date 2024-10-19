using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Http.HttpResults;
using Booking.Application.Data;
using Booking.Application.Exceptions;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Bookings.Commands.DeleteBooking;

internal class DeleteBookingHandler(IBookingDbContext dbContext) : ICommandHandler<DeleteBookingCommand, DeleteBookingResult>
{
    public async Task<DeleteBookingResult> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await dbContext.Bookings.FindAsync(BookingId.Of(request.BookingId));

        if (booking is null)
        {
            throw new BookingNotFoundException(request.BookingId);
        }

        dbContext.Bookings.Remove(booking);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBookingResult(true);
    }
}
