using Reservation.API.Excepitons;

namespace Reservation.API.Data
{
    public class ReservationRepository(IDocumentSession session) : IReservationRepository
    {
        public async Task<ReservationModel> GetReservation(Guid userId, CancellationToken cancellationToken = default)
        {
            var reservation = await session.LoadAsync<ReservationModel>(userId) ?? throw new ReservationNotFoundException("Reservation", userId.ToString());

            return reservation;
        }

        public async Task<ReservationModel> StoreReservation(ReservationModel cart, CancellationToken cancellationToken = default)
        {
            cart.Id = Guid.NewGuid();
            try
            {

                session.Store(cart);
                await session.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
            return cart;
        }

        public async Task<bool> DeleteReservation(Guid userId, CancellationToken cancellationToken = default)
        {
            var reservation = await session.Query<ReservationModel>().Where(x => x.GuestId == userId).FirstOrDefaultAsync();
            session.Delete<ReservationModel>(userId);

            await session.SaveChangesAsync();

            return true;
        }
    }
}
