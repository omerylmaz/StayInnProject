using BuildingBlocks.Messaging.Events;
using MassTransit;
using Reservation.API.Data;
using Reservation.API.DTOs;

namespace Reservation.API.Reservation.CheckoutReservation;

public record ConfirmReservationCommand(ConfirmReservationDto ConfirmReservation) : ICommand<ConfirmReservationResult>;

public record ConfirmReservationResult(bool IsSuccess);

public class ConfirmReservationHandler(IReservationRepository reservationRepository,
   IPublishEndpoint publishEndpoint) 
    : ICommandHandler<ConfirmReservationCommand, ConfirmReservationResult>
{
    public async Task<ConfirmReservationResult> Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetReservation(request.ConfirmReservation.GuestId);
        if (reservation == null)
        {
            return new ConfirmReservationResult(false);
        }
        request.ConfirmReservation.TotalPrice = reservation.TotalPrice;

        var eventMessage = request.ConfirmReservation.Adapt<ConfirmReservationEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await reservationRepository.DeleteReservation(request.ConfirmReservation.GuestId, cancellationToken);
        return new ConfirmReservationResult(true);
    }
}
