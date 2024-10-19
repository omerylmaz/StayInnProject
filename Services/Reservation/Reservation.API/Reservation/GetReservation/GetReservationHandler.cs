using Reservation.API.Data;
using BuildingBlocks.CQRS;
using Marten;

namespace Reservation.API.Reservation.GetReservation;

public record GetReservationQuery(Guid UserId) : IQuery<GetReservationResult>;

public record GetReservationResult(ReservationModel ReservationCart);

public class GetReservationHandler(IReservationRepository reservationRepository) : IQueryHandler<GetReservationQuery, GetReservationResult>
{
    public async Task<GetReservationResult> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var response = await reservationRepository.GetReservation(request.UserId, cancellationToken);

        return new(response);
    }
}
