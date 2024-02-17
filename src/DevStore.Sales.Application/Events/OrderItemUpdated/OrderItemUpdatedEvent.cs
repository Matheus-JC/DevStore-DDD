using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.OrderItemUpdated;

public class OrderItemUpdatedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public OrderItemUpdatedEvent(Guid orderId, Guid clientId, Guid productId, int quantity)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        ProductId = productId;
        Quantity = quantity;
    }
}
