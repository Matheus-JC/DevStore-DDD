using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.OrderItemRemoved;

public class OrderItemRemovedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }

    public OrderItemRemovedEvent(Guid orderId, Guid clientId, Guid productId) 
    { 
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        ProductId = productId;
    }
}
