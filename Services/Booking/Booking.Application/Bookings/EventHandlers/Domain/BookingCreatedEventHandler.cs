using Booking.Application.Extensions;
using Booking.Domain.DomainEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Booking.Application.Bookings.EventHandlers.Domain;

internal sealed class BookingCreatedEventHandler(
    ILogger<BookingCreatedEventHandler> logger,
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager) : INotificationHandler<BookingCreatedEvent>
{
    public async Task Handle(BookingCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Booking created: {BookingId}", domainEvent.Booking.Id.Value);

        if (await featureManager.IsEnabledAsync("BookingFulfilment"))
        {
            var integrationEvent = domainEvent.Booking.ConvertToBookingDto();
            await publishEndpoint.Publish(domainEvent, cancellationToken);
        }
    }
}
