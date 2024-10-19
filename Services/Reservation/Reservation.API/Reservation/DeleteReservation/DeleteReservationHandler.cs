
using Reservation.API.Data;

namespace Reservation.API.Reservation.DeleteReservation;

public record DeleteReservationCommand(Guid UserId) : ICommand<DeleteReservationResult>;

public record DeleteReservationResult(bool IsSuccess);

public class DeleteReservationHandler(IReservationRepository reservationRepository) : ICommandHandler<DeleteReservationCommand, DeleteReservationResult>
{
    public async Task<DeleteReservationResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var response = await reservationRepository.DeleteReservation(request.UserId, cancellationToken);

        return new(response);
    }
}
