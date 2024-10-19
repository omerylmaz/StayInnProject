using Booking.Domain.ValueObjects;

namespace Booking.Application.Dtos;

public record PaymentDto(decimal Amount, DateTime PaymentDate, PaymentStatus Status, PaymentMethod Method);
