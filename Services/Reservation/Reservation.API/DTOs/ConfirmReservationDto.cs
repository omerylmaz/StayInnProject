using Reservation.API.DTO;

namespace Reservation.API.DTOs;

public class ConfirmReservationDto
{
    public Guid ReservationId { get; set; } = default!;
    public Guid GuestId { get; set; } = default!;
    //public Guid HotelId { get; set; } = default!;
    public Guid RoomId { get; set; } = default!;
    public DateTime CheckInDate { get; set; } = default!;
    public DateTime CheckOutDate { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
    public int NightQuantity { get; set; } = default!;
    public PaymentDto Payment { get; set; } = default!;
    public AddressDto Address { get; set; } = default!;
}
