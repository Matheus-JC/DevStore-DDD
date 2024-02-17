using DevStore.Common.DomainObjects;
using DevStore.Sales.Domain.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DevStore.Sales.Domain.Tests;

public class OrderItemTests(OrderItemFixture orderItemFixture) : IClassFixture<OrderItemFixture>
{
    public readonly OrderItemFixture _orderItemFixture = orderItemFixture;

    [Fact]
    public void CreateOrderItem_WithEmptyProductId_ShouldThrowDomainException()
    {
        // Arrange & Act & Assert
        FluentActions.Invoking(() => _orderItemFixture.CreateOrderItemWithAnInvalidProperty(nameof(OrderItem.ProductId)))
            .Should().Throw<DomainException>().WithMessage($"*{nameof(OrderItem.ProductId)}*");
    }

    [Fact]
    public void AssociateOrder_WithValidOrderId_ShouldAssociateOrderIdCorrectly()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem();
        var orderId = Guid.NewGuid();

        // Act
        orderItem.AssociateOrder(orderId);

        // Assert
        orderItem.OrderId.Should().Be(orderId);
    }

    [Fact]
    public void AssociateOrder_WithEmptyOrderId_ShouldThrowDomainException()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem();
        var orderId = Guid.Empty;

        // Act & Assert
        FluentActions.Invoking(() => orderItem.AssociateOrder(orderId: orderId))
            .Should().Throw<DomainException>().WithMessage("*empty*");
    }

    [Fact]
    public void CalculateValue_ShouldCalculateTotalValueCorrectly()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem(quantity: 10, unitValue: 10.0m);

        // Act
        var totalValue = orderItem.CalculateValue();

        // Assert
        totalValue.Should().Be(100.0m);
    }

    [Fact]
    public void AddUnits_WithPositiveUnitValue_ShouldExecuteCorrectly()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem(quantity: 10);

        // Act
        orderItem.AddUnits(units: 5);

        // Assert
        orderItem.Quantity.Should().Be(15);
    }

    [Fact]
    public void RemoveUnits_WithValidUnitValue_ShouldExecuteCorrectly()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem(quantity: 10);

        // Act
        orderItem.RemoveUnits(units: 2);

        // Assert
        orderItem.Quantity.Should().Be(8);
    }

    [Fact]
    public void UpdateQuantity_WithPositiveValue_ShouldExecuteCorrectly()
    {
        // Arrange
        var orderItem = _orderItemFixture.CreateOrderItem(quantity: 10);
        var newQuantity = 20;

        // Act
        orderItem.UpdateQuantity(newQuantity);

        // Assert
        orderItem.Quantity.Should().Be(newQuantity);
    }
}
