using Bogus;

namespace DevStore.Sales.Domain.Tests.Fixtures;

public class OrderItemFixture
{
    private readonly Faker _faker = new("en_US");

    public OrderItem CreateOrderItem(int? quantity = null, decimal? unitValue = null)
    {
        return new OrderItem(
            productId: Guid.NewGuid(),
            productName: GenerateRandomProductName(),
            quantity: quantity ?? GenerateRandomQuantity(),
            unitValue: unitValue ?? GenerateRandomValue()
        );
    }

    public OrderItem CreateOrderItemWithAnInvalidProperty(string invalidPropertyName) 
    {
        return new OrderItem(
            productId: invalidPropertyName == nameof(OrderItem.ProductId) ? Guid.Empty : Guid.NewGuid(),
            productName: invalidPropertyName == nameof(OrderItem.ProductName) ? "" : GenerateRandomProductName(),
            quantity: invalidPropertyName == nameof(OrderItem.Quantity) ? 0 : GenerateRandomQuantity(),
            unitValue: invalidPropertyName == nameof(OrderItem.UnitValue) ? 0.0m : GenerateRandomValue()
        );
    }

    public string GenerateRandomProductName() =>
        _faker.Commerce.ProductName();

    public int GenerateRandomQuantity() =>
        _faker.Random.Number(1, 100);

    public decimal GenerateRandomValue() =>
        _faker.Random.Decimal(1.0m, 1000.0m);
}
