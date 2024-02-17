using MediatR;

namespace DevStore.Sales.Application.Events.OrderUpdated;

public class OrderUpdatedHandler : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
