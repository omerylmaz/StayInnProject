using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Reservation.API.Data
{
    public class CachedReservationRepository
        (IReservationRepository reservationRepository,
        IDistributedCache cache) : IReservationRepository
    {
        public async Task<ReservationModel> GetReservation(Guid userId, CancellationToken cancellationToken = default)
        {
            var reservationCached = await cache.GetStringAsync(userId.ToString(), cancellationToken);
            if (reservationCached is null)
            {
                var reservation = await reservationRepository.GetReservation(userId, cancellationToken);
                await cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize(reservation), cancellationToken);
                return reservation;
            }
            return JsonSerializer.Deserialize<ReservationModel>(reservationCached)!;
        }

        public async Task<ReservationModel> StoreReservation(ReservationModel cart, CancellationToken cancellationToken = default)
        {
            var response = await reservationRepository.StoreReservation(cart, cancellationToken);
            await cache.SetStringAsync(cart.GuestId.ToString(), JsonSerializer.Serialize(cart), cancellationToken);
            return response;
        }
        public async Task<bool> DeleteReservation(Guid userId, CancellationToken cancellationToken = default)
        {
            var response = await reservationRepository.DeleteReservation(userId, cancellationToken);
            await cache.RemoveAsync(userId.ToString(), cancellationToken);

            return response;
        }
    }
}
