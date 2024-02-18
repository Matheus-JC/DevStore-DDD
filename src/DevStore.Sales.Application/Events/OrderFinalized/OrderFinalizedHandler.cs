using MediatR;

namespace DevStore.Sales.Application.Events.OrderFinalized;

public class OrderFinalizedHandler : INotificationHandler<OrderFinalizedEvent>
{
    public Task Handle(OrderFinalizedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
