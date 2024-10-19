namespace Booking.Application.Dtos;

public record BookingItemDto(Guid BookingId, Guid RoomId, int NightQuantity, decimal Price);
