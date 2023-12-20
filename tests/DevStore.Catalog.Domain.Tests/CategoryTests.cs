using DevStore.Catalog.Domain.Tests.Fixtures;
using DevStore.Common.DomainObjects;
using FluentAssertions;
using Xunit;

namespace DevStore.Catalog.Domain.Tests;

public class CategoryTests(CategoryFixture categoryFixture) : IClassFixture<CategoryFixture>
{
    private readonly CategoryFixture _categoryFixture = categoryFixture;

    [Fact]
    public void CreateCategory_WithEmptyName_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _categoryFixture.CreateInvalidCategoryWithEmptyName())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Category.Name)}*");
    }

    [Fact]
    public void CreateCategory_WithZeroCode_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _categoryFixture.CreateInvalidCategoryWithZeroCode())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Category.Code)}*");
    }

    [Fact]
    public void ToString_InValidCategory_ShouldReturnExpectedFormat()
    {
        // Arrange
        var category = _categoryFixture.CreateValidCategory();

        // Act
        var categoryToString = category.ToString();

        // Assert
        categoryToString.Should().Be($"{category.Name} - {category.Code}");

    }
}
