using Reservation.API.Data.Enums;

namespace Reservation.API.Data.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid GuestId { get; set; }
    public Guid RoomId { get; set; }

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }

    public ReservationStatus Status { get; set; }
    public string CouponName { get; set; }

    //public Guid PaymentId { get; set; }
}