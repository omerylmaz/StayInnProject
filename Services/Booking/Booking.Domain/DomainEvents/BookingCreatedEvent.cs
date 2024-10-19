using Booking.Domain.Abstractions;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Domain.DomainEvents;

public record BookingCreatedEvent(BookingModel Booking) : IDomainEvent;