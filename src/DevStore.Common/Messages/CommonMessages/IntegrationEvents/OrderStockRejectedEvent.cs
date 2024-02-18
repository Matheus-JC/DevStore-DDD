namespace DevStore.Common.Messages.CommonMessages.IntegrationEvents;

public class OrderStockRejectedEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }

    public OrderStockRejectedEvent(Guid orderId, Guid clientId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
    }
}
