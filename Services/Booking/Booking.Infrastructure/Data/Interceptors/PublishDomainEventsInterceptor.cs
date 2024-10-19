using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Booking.Domain.Abstractions;

namespace Booking.Infrastructure.Data.Interceptors;

internal class PublishDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(x => x.Entity.Events.Any())
            .Select(x => x.Entity);

        var domainEvents = aggregates
            .SelectMany(x => x.Events)
            .ToList();

        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}