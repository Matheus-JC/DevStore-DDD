using DevStore.Catalog.Domain.Tests.Fixtures;
using DevStore.Common.DomainObjects;
using FluentAssertions;
using Xunit;

namespace DevStore.Catalog.Domain.Tests;

public class DimensionsTests(DimensionsFixture dimensionsFixture) : IClassFixture<DimensionsFixture>
{
    private readonly DimensionsFixture _dimensionsFixture = dimensionsFixture;

    [Fact]
    public void CreateDimension_WithZeroWidth_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _dimensionsFixture.CreateInvalidDimensionsWithWidthtZero())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Dimensions.Width)}*");
    }

    [Fact]
    public void CreateDimension_WithZeroHeight_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _dimensionsFixture.CreateInvalidDimensionsWithHeightZero())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Dimensions.Height)}*");
    }

    [Fact]
    public void CreateDimension_WithZeroDepth_ShouldThrowDomainException()
    {
        //Arrange & Act & Assert
        FluentActions.Invoking(() => _dimensionsFixture.CreateInvalidDimensionsWithDepthZero())
            .Should().Throw<DomainException>().WithMessage($"*{nameof(Dimensions.Depth)}*");
    }

    [Fact]
    public void ToString_InValidDimensions_ShouldReturnExpectedFormat()
    {
        // Arrange
        var dimensions = _dimensionsFixture.CreateValidDimensions();

        // Act
        var dimensionsToString = dimensions.ToString();

        // Assert
        dimensionsToString.Should().Be($"WxHxD: {dimensions.Width} x {dimensions.Height} x {dimensions.Depth}");

    }
}
