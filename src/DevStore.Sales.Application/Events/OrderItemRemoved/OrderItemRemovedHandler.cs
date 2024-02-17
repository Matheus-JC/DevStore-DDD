using MediatR;

namespace DevStore.Sales.Application.Events.OrderItemRemoved;

public class OrderItemRemovedHandler : INotificationHandler<OrderItemRemovedEvent>
{
    public Task Handle(OrderItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
