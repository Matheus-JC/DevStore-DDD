using DevStore.Common.Communication.Mediator;
using DevStore.Common.DomainObjects;

namespace DevStore.Sales.Data;

public static class MediatorExtensions
{
    public static async Task PublishEvents(this IMediatorHandler mediator, SalesContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Count != 0);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}
