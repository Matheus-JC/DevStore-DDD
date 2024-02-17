using DevStore.Common.DomainObjects;

namespace DevStore.Sales.Domain;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal UnitValue { get; private set; }

    public Order? Order { get; private set; }

    // EF
    private OrderItem() {}

    public OrderItem(Guid productId, string productName, int quantity, decimal unitValue) 
    {
        AssertionConcern.AssertArgumentNotEquals(productId, Guid.Empty,
            $"{nameof(productId)} cannot be empty");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitValue = unitValue;
    }

    public void AssociateOrder(Guid orderId)
    {
        AssertionConcern.AssertArgumentNotEquals(orderId, Guid.Empty,
            $"{nameof(orderId)} cannot be empty");

        OrderId = orderId;
    }

    public decimal CalculateValue()
    {
        return Quantity * UnitValue;
    }

    public void AddUnits(int units)
    {
        Quantity += units;
    }

    public void RemoveUnits(int units)
    {
        Quantity -= units;
    }

    public void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;
    }
}
