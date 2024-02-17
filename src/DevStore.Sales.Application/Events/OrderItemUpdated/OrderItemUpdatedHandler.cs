using MediatR;

namespace DevStore.Sales.Application.Events.OrderItemUpdated;

public class OrderItemUpdatedHandler : INotificationHandler<OrderItemUpdatedEvent>
{
    public Task Handle(OrderItemUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
