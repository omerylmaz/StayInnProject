using BuildingBlocks.CQRS;
using Booking.Application.Data;
using Booking.Application.Extensions;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Bookings.Commands.CreateBooking;

internal class CreateBookingHandler(IBookingDbContext dbContext) : ICommandHandler<CreateBookingCommand, CreateBookingResult>
{
    public async Task<CreateBookingResult> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        //var requestWithId = request with { Booking with { } }
        var booking = request.Booking.ConvertToBooking();
        await dbContext.Bookings.AddAsync(booking, cancellationToken);
        try
        {
        await dbContext.SaveChangesAsync(cancellationToken);

        }
          catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
        return new(booking.Id.Value);
    }
}