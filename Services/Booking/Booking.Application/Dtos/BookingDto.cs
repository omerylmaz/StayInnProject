using Booking.Domain.Enums;

namespace Booking.Application.Dtos;

public record BookingDto(
    Guid CustomerId,
    string BookingName,
    AddressDto BillingAddress,
    PaymentDto Payment,
    BookingStatus Status,
    Guid RoomId,
    int NightQuantity,
    decimal TotalPrice,
    DateTime BookingDate
    );


