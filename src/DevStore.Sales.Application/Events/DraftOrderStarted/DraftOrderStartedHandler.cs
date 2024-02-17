using MediatR;

namespace DevStore.Sales.Application.Events.DraftOrderStarted;

public class DraftOrderStartedHandler : INotificationHandler<DraftOrderStartedEvent>
{
    public Task Handle(DraftOrderStartedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
