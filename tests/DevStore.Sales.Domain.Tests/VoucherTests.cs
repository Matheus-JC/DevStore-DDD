using DevStore.Sales.Domain.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace DevStore.Sales.Domain.Tests;

public class VoucherTests(VoucherFixture voucherFixture) : IClassFixture<VoucherFixture>
{
    private readonly VoucherFixture _voucherFixture = voucherFixture;

    [Fact]
    public void IsApplicable_WithValidPercentageDiscountVoucher_ShouldReturnAsValid()
    {
        // Arrange
        var voucher = _voucherFixture.CreateVoucher(VoucherDiscountType.Percentage);

        // Act
        var result = voucher.IsApplicable();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }

    [Fact]
    public void IsApplicable_WithValidFixedDiscountVoucher_ShouldReturnAsValid()
    {
        // Arrange
        var voucher = _voucherFixture.CreateVoucher(VoucherDiscountType.Fixed);

        // Act
        var result = voucher.IsApplicable();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Messages.Should().BeEmpty();
    }

    [Fact]
    public void IsApplicable_WithExpiredDate_ShouldReturnAsInvalid()
    {
        // Arrange
        var voucher = _voucherFixture.CreateVoucher(
            voucherDiscountType: VoucherDiscountType.Fixed,
            validityDate: _voucherFixture.GenerateRandomExpiredDate()
        );

        // Act
        var result = voucher.IsApplicable();

        // Assert
        result.IsValid.Should().BeFalse();
        result.Messages.Should().HaveCount(1);
        result.Messages[0].Should().Contain("expired");
    }
}
