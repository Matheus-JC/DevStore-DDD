using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.OrderFinalized;

public class OrderFinalizedEvent : Event
{
    public Guid OrderId { get; private set; }

    public OrderFinalizedEvent(Guid orderId)
    {
        AggregateId = orderId;
        OrderId = orderId;
    }
}
