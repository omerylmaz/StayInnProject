using BuildingBlocks.CQRS;
using Booking.Application.Data;
using Booking.Application.Extensions;
using Booking.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Booking.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Booking.Application.Exceptions;

namespace Booking.Application.Bookings.Commands.UpdateBooking;

internal class UpdateBookingHandler(IBookingDbContext dbContext) : ICommandHandler<UpdateBookingCommand, UpdateBookingResult>
{
    public async Task<UpdateBookingResult> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingDb = await dbContext.Bookings.FindAsync(BookingId.Of(request.Id));
        if (bookingDb is null)
        {
            throw new BookingNotFoundException(request.Id);
        }
        UpdateBookingWithNewValues(bookingDb, request.Booking);

        await dbContext.SaveChangesAsync(cancellationToken);


        return new UpdateBookingResult(true);
    }

    private void UpdateBookingWithNewValues(BookingModel booking, BookingDto bookingDto)
    {
        //var updatedShippingAddress = Address.Of(bookingDto.ShippingAddress.FirstName, bookingDto.ShippingAddress.LastName, bookingDto.ShippingAddress.EmailAddress, bookingDto.ShippingAddress.AddressLine, bookingDto.ShippingAddress.Country, bookingDto.ShippingAddress.State, bookingDto.ShippingAddress.ZipCode);
        var updatedBillingAddress = Address.Of(bookingDto.BillingAddress.FirstName, bookingDto.BillingAddress.LastName, bookingDto.BillingAddress.EmailAddress, bookingDto.BillingAddress.AddressLine, bookingDto.BillingAddress.Country, bookingDto.BillingAddress.State, bookingDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(bookingDto.Payment.CardName, bookingDto.Payment.CardNumber, bookingDto.Payment.Expiration, bookingDto.Payment.Cvv, bookingDto.Payment.PaymentMethod);

        booking.Update(
            bookingName: bookingDto.BookingName,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            roomId: RoomId.Of(bookingDto.RoomId),
            nightQuantity: bookingDto.NightQuantity,
            status: bookingDto.Status);
    }
}
