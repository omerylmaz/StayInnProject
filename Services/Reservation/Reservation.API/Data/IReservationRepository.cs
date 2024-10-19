namespace Reservation.API.Data;

public interface IReservationRepository
{
    public Task<ReservationModel> GetReservation(Guid userId, CancellationToken cancellationToken = default);
    public Task<ReservationModel> StoreReservation(ReservationModel cart, CancellationToken cancellationToken = default);
    public Task<bool> DeleteReservation(Guid userId, CancellationToken cancellationToken = default);
}
