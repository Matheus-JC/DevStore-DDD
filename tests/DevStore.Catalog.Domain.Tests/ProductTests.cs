using DevStore.Catalog.Domain.Tests.Fixtures;
using DevStore.Common.DomainObjects;
using FluentAssertions;
using Xunit;

namespace DevStore.Catalog.Domain.Tests;

public class ProductTests(ProductFixture productFixture) : IClassFixture<ProductFixture>
{
    private readonly ProductFixture _productFixture = productFixture;

    [Fact]
    public void CreateProduct_WithoutName_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _productFixture.CreateInvalidProductWithoutName())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.Name)}*");
    }

    [Fact]
    public void CreateProduct_WihoutDescription_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _productFixture.CreateInvalidProductWithoutDescription())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.Description)}*");
    }

    [Fact]
    public void CreateProduct_WithZeroPrice_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _productFixture.CreateInvalidProductWithZeroPrice())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.Price)}*");
    }

    [Fact]
    public void CreateProduct_WithEmptyCategoryId_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _productFixture.CreateInvalidProductWithEmptyCategoryId())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.CategoryId)}*");
    }

    [Fact]
    public void CreateProduct_WithNoImage_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _productFixture.CreateInvalidProductWithNoImage())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.Image)}*");
    }

    [Fact]
    public void Activate_ADeactivatedProduct_ShouldActivateProduct()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct(active: false);

        // Act
        product.Activate();

        // Assert
        product.Active.Should().BeTrue();
    }

    [Fact]
    public void Activate_AnActivatedProduct_ShouldKeepProductActive()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();

        // Act
        product.Activate();

        // Assert
        product.Active.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_AnActivedProduct_ShouldDeactivateProduct()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();

        // Act
        product.Deactivate();

        // Assert
        product.Active.Should().BeFalse();
    }

    [Fact]
    public void Deactivate_ADeactivatedProduct_ShouldKeepProductDeactive()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct(active: false);

        // Act
        product.Deactivate();

        // Assert
        product.Active.Should().BeFalse();
    }

    [Fact]
    public void SetCategory_WithValidValues_ShouldChangeCategory()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();
        var category = new Category("Foo", 10);

        // Act
        product.SetCategory(category);

        // Assert
        product.Category.Should().Be(category);
        product.CategoryId.Should().Be(category.Id);
    }

    [Fact]
    public void SetDescription_WithValidValue_ShouldChangeDescription()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();
        var newDescription = "New Description";

        // Act

        product.SetDescription(newDescription);

        // Assert
        product.Description.Should().Be(newDescription);
    }

    [Fact]
    public void SetDescription_WithEmptyValue_ShouldThrowDomainException()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();
        var newDescription = "";

        // Act & Assert
        FluentActions.Invoking(() => product.SetDescription(newDescription))
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Product.Description)}*");

    }

    [Fact]
    public void HasQuantityInStock_WithAQuantityBelowStock_ShouldReturnTrue()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act
        var hasQuantityInStock = product.HasQuantityInStock(quantity: 5);

        // Assert
        hasQuantityInStock.Should().BeTrue();
    }

    [Fact]
    public void HasQuantityInStock_WithAQuantityAboveStock_ShouldReturnFalse()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act
        var hasQuantityInStock = product.HasQuantityInStock(quantity: 15);

        // Assert
        hasQuantityInStock.Should().BeFalse();
    }

    [Fact]
    public void DebitStock_WithValidValue_ShouldDebitStockCorrectly()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act
        product.DebitStock(quantity: 2);

        // Assert
        product.Stock.Should().Be(8);
    }

    [Fact]
    public void DebitStock_WithZeroQuantity_ShouldThrowDomainException()
    {
        // Arrange
        var product = _productFixture.CreateValidProduct();

        // Act && Assert
        FluentActions.Invoking(() => product.DebitStock(0))
            .Should().Throw<DomainException>().WithMessage("*zero*");
    }

    [Fact]
    public void DebitStock_WithAboveQuantityStock_ShouldThrowDomainException()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act && Assert
        FluentActions.Invoking(() => product.DebitStock(11))
            .Should().Throw<DomainException>().WithMessage("*insufficient*");
    }

    [Fact]
    public void DebitStock_WithTotalQuantity_ShouldLeaveProductWithZeroStock()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act
        product.DebitStock(quantity: 10);

        // Assert
        product.Stock.Should().Be(0);
    }

    [Fact]
    public void ReplenishStock_WithValidQuantity_ShouldReplenishStockCorrectly()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act
        product.ReplenishStock(5);

        // Assert
        product.Stock.Should().Be(15);
    }

    [Fact]
    public void ReplenishStock_WithNegativeQuantity_ShouldThrowDomainException()
    {
        // Arrange
        var product = _productFixture.CreateValidProductWithSpecifiedStock(stock: 10);

        // Act && Assert
        FluentActions.Invoking(() => product.ReplenishStock(-1))
            .Should().Throw<DomainException>().WithMessage("*negative*");
    }
}
