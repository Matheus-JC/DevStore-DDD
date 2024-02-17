using Bogus;

namespace DevStore.Sales.Domain.Tests.Fixtures;

public class VoucherFixture
{
    private readonly Faker _faker = new("en_US");

    public Voucher CreateVoucher(VoucherDiscountType voucherDiscountType, string? code = null, 
        decimal? discountValue = null, int? quantity = null, DateTime? validityDate = null)
    {
        discountValue ??= voucherDiscountType == VoucherDiscountType.Percentage
            ? GenerateRandomDiscountPercentage()
            : GenerateRandomFixedDiscountValue();

        return new Voucher(
            discountType: voucherDiscountType,
            code: code ?? GenerateRandomCode(),
            quantity: quantity ?? GenerateRandomQuantity(),
            validityDate: validityDate ?? GenerateRandomValidExpirationDate(),
            discountValue: (decimal) discountValue
        );
    }

    public string GenerateRandomCode() =>
        _faker.Lorem.Word();

    public decimal GenerateRandomDiscountPercentage() =>
        _faker.Random.Decimal(1.0m, 50.0m);

    public decimal GenerateRandomFixedDiscountValue() =>
        _faker.Random.Decimal(1.0m, 200.0m);

    public int GenerateRandomQuantity() =>
        _faker.Random.Number(1, 100);

    public DateTime GenerateRandomValidExpirationDate() =>
        _faker.Date.Future();

    public DateTime GenerateRandomExpiredDate() =>
        _faker.Date.Past();
}
