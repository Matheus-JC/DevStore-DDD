using DevStore.Catalog.Domain.Tests.Fixtures;
using DevStore.Common.Data;
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
    public async void DebitStock_WithValidParams_ShouldExecuteSuccessfully()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 15);
        var mockerProductRepository = _mocker.GetMock<IProductRepository>();
        var mockerUnitOfWork = _mocker.GetMock<IUnitOfWork>();

        mockerProductRepository.Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        mockerUnitOfWork.Setup(u => u.Commit()).ReturnsAsync(true);

        // Act
        var result = await _stockService.DebitStock(product.Id, quantity: 10);

        // Assert
        result.Should().BeTrue();
        product.Stock.Should().Be(5);
        mockerProductRepository.Verify(r => r.GetById(product.Id), Times.Once);
        mockerProductRepository.Verify(r => r.Update(product), Times.Once);
        mockerUnitOfWork.Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async void DebitStock_WithInvalidProductId_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 15);
        var mockerProductRepository = _mocker.GetMock<IProductRepository>();
        var mockerUnitOfWork = _mocker.GetMock<IUnitOfWork>();
        var productInvalidId = Guid.NewGuid();
        
        mockerProductRepository.Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.DebitStock(productInvalidId, quantity: 10);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(15);
        mockerProductRepository.Verify(r => r.GetById(productInvalidId), Times.Once);
        mockerProductRepository.Verify(r => r.Update(product), Times.Never);
        mockerUnitOfWork.Verify(r => r.Commit(), Times.Never);
    }

    [Fact]
    public async void DebitStock_WithInsuficientStock_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 15);
        var mockerProductRepository = _mocker.GetMock<IProductRepository>();
        var mockerUnitOfWork = _mocker.GetMock<IUnitOfWork>();

        mockerProductRepository.Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.DebitStock(product.Id, quantity: 20);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(15);
        mockerProductRepository.Verify(r => r.GetById(product.Id), Times.Once);
        mockerProductRepository.Verify(r => r.Update(product), Times.Never);
        mockerUnitOfWork.Verify(r => r.Commit(), Times.Never);
    }

    [Fact]
    public async void ReplenishStock_WithValidParams_ShouldExecuteSuccessfully()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 5);
        var mockerProductRepository = _mocker.GetMock<IProductRepository>();
        var mockerUnitOfWork = _mocker.GetMock<IUnitOfWork>();

        mockerProductRepository.Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        mockerUnitOfWork.Setup(u => u.Commit()).ReturnsAsync(true);

        // Act
        var result = await _stockService.ReplenishStock(product.Id, quantity: 10);

        // Assert
        result.Should().BeTrue();
        product.Stock.Should().Be(15);
        mockerProductRepository.Verify(r => r.GetById(product.Id), Times.Once);
        mockerProductRepository.Verify(r => r.Update(product), Times.Once);
        mockerUnitOfWork.Verify(r => r.Commit(), Times.Once);
    }

    [Fact]
    public async void ReplenishStock_WithInvalidProductId_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 5);
        var mockerProductRepository = _mocker.GetMock<IProductRepository>();
        var mockerUnitOfWork = _mocker.GetMock<IUnitOfWork>();

        var productInvalidId = Guid.NewGuid();

        mockerProductRepository.Setup(p => p.GetById(product.Id))
            .ReturnsAsync(product);

        // Act
        var result = await _stockService.ReplenishStock(productInvalidId, quantity: 10);

        // Assert
        result.Should().BeFalse();
        product.Stock.Should().Be(5);
        mockerProductRepository.Verify(r => r.GetById(productInvalidId), Times.Once);
        mockerProductRepository.Verify(r => r.Update(product), Times.Never);
        mockerUnitOfWork.Verify(r => r.Commit(), Times.Never);
    }
}
