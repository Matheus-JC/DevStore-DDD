using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.OrderItemAdded;

public class OrderItemAddedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitValue { get; private set; }
    public int Quantity { get; private set; }

    public OrderItemAddedEvent(Guid orderId, Guid clientId, Guid productId, string productName,
        decimal unitValue, int quantity)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        ProductId = productId;
        ProductName = productName;
        UnitValue = unitValue;
        Quantity = quantity;
    }
}
