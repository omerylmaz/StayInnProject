using Booking.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Bookings.EventHandlers.Domain
{
    internal sealed class BookingUpdateEventHandler(ILogger<BookingUpdateEventHandler> logger) : INotificationHandler<BookingUpdatedEvent>
    {
        public Task Handle(BookingUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Booking updated: {BookingId}", notification.BookingModel.Id.Value);
            return Task.CompletedTask;
        }
    }
}
