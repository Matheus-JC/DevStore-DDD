using MediatR;

namespace DevStore.Sales.Application.Events.OrderItemAdded;

public class OrderItemAddedHandler : INotificationHandler<OrderItemAddedEvent>
{
    public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
