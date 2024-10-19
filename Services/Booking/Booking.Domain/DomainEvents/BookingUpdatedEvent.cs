using Booking.Domain.Abstractions;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Domain.DomainEvents;

public record BookingUpdatedEvent(BookingModel BookingModel) : IDomainEvent;
