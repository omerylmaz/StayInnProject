using Booking.Application.Bookings.Commands.CreateBooking;
using Booking.Domain.ValueObjects;
using Booking.Application.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Address = Booking.Domain.ValueObjects.Address;
using Payment = Booking.Domain.ValueObjects.Payment;
using Booking.Domain.Enums;

namespace Booking.Application.Bookings.EventHandlers.Integration;

public class ConfirmReservationHandler(ISender sender) : IConsumer<ConfirmReservationEvent>
{
    public async Task Consume(ConsumeContext<ConfirmReservationEvent> context)
    {
        var orderDb = ConvertToBookingModel(context.Message);
        await sender.Send(orderDb);
    }

    private CreateBookingCommand ConvertToBookingModel(ConfirmReservationEvent confirmReservationEvent)
    {
        var address = new AddressDto(
            confirmReservationEvent.Address.FirstName,
            confirmReservationEvent.Address.LastName,
            confirmReservationEvent.Address.EmailAddress,
            confirmReservationEvent.Address.AddressLine,
            confirmReservationEvent.Address.Country,
            confirmReservationEvent.Address.State,
            confirmReservationEvent.Address.ZipCode
            );

        var payment = new PaymentDto(
            confirmReservationEvent.Payment.CardName,
            confirmReservationEvent.Payment.CardNumber,
            confirmReservationEvent.Payment.Expiration,
            confirmReservationEvent.Payment.CVV,
            confirmReservationEvent.Payment.PaymentMethod
            );

        return new CreateBookingCommand(
            new BookingDto(
            confirmReservationEvent.UserId,
            $"Booking:{confirmReservationEvent.RoomId}",
            address,
            payment,
            BookingStatus.Completed,
            confirmReservationEvent.RoomId,
            confirmReservationEvent.NightQuantity,
            confirmReservationEvent.TotalPrice,
            DateTime.Now
            ));
    }
}
