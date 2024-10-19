using BuildingBlocks.Exceptions;

namespace Reservation.API.Excepitons
{
    public class ReservationNotFoundException(string name, string key) : NotFoundException(name, key)
    {
    }
}
