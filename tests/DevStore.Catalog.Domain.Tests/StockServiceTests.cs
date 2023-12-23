using DevStore.Catalog.Domain.Tests.Fixtures;
using DevStore.Common.Communication;
using DevStore.Common.Data;
using DevStore.Common.DomainObjects;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace DevStore.Catalog.Domain.Tests;

public class StockServiceTests: IClassFixture<ProductFixture>
{
    private readonly ProductFixture _productFixture;
    private readonly AutoMocker _mocker;
    private readonly StockService _stockService;

    public StockServiceTests(ProductFixture productFixture)
    {
        _productFixture = productFixture;
        _mocker = new AutoMocker();
        _stockService = _mocker.CreateInstance<StockService>();
    }

    [Fact]
    public async void DebitStock_WithStockNotGettingLow_ShouldReturnTrue()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(
            stock: StockService.LowQuantityStock + 10
        );

        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        _mocker.GetMock<IUnitOfWork>().Setup(u => u.Commit()).ReturnsAsync(true);

        // Act
        var result = await _stockService.DebitStock(product.Id, quantity: 5);

        // Assert
        result.Should().BeTrue();
        product.Stock.Should().Be(StockService.LowQuantityStock + 5);

        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(product.Id), Times.Once);
        _mocker.GetMock<IProductRepository>().Verify(r => r.Update(product), Times.Once);
        _mocker.GetMock<IMediatorHandler>().Verify(m => m.PublishEvent(It.IsAny<DomainEvent>()), Times.Never);
        _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async void DebitStock_WithStockGettingLow_ShouldReturnTrueAndSendDomainEvent()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(
            stock: StockService.LowQuantityStock + 10
        );

        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        _mocker.GetMock<IUnitOfWork>().Setup(u => u.Commit()).ReturnsAsync(true);

        // Act
        var result = await _stockService.DebitStock(product.Id, quantity: 15);

        // Assert
        result.Should().BeTrue();
        product.Stock.Should().Be(StockService.LowQuantityStock - 5);
        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(product.Id), Times.Once);
        _mocker.GetMock<IProductRepository>().Verify(r => r.Update(product), Times.Once);
        _mocker.GetMock<IMediatorHandler>().Verify(m => m.PublishEvent(It.IsAny<DomainEvent>()), Times.Once);
        _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async void DebitStock_WithInvalidProductId_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 15);
        var productInvalidId = Guid.NewGuid();
        
        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.DebitStock(productInvalidId, quantity: 10);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(15);
        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(productInvalidId), Times.Once);
    }

    [Fact]
    public async void DebitStock_WithInsuficientStock_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 15);

        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.DebitStock(product.Id, quantity: 20);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(15);
        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(product.Id), Times.Once);
    }

    [Fact]
    public async void ReplenishStock_WithValidParams_ShouldReturnTrue()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 5);

        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        _mocker.GetMock<IUnitOfWork>().Setup(u => u.Commit()).ReturnsAsync(true);

        // Act
        var result = await _stockService.ReplenishStock(product.Id, quantity: 10);

        // Assert
        result.Should().BeTrue();
        product.Stock.Should().Be(15);
        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(product.Id), Times.Once);
        _mocker.GetMock<IProductRepository>().Verify(r => r.Update(product), Times.Once);
        _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async void ReplenishStock_WithInvalidProductId_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 5);

        var productInvalidId = Guid.NewGuid();

        _mocker.GetMock<IProductRepository>().Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.ReplenishStock(productInvalidId, quantity: 10);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(5);
        _mocker.GetMock<IProductRepository>().Verify(r => r.GetById(productInvalidId), Times.Once);
    }
}
