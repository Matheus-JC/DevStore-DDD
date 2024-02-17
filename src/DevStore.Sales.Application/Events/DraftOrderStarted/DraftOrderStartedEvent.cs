using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.DraftOrderStarted;

public class DraftOrderStartedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }

    public DraftOrderStartedEvent(Guid orderId, Guid clientId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
    }
}
