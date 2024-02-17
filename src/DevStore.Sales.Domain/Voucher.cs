using DevStore.Common.DomainObjects;

namespace DevStore.Sales.Domain;

public class Voucher : Entity
{
    public string Code { get; private set; }
    public VoucherDiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public DateTime? UsageDate { get; private set; }
    public DateTime ValidityDate { get; private set; }
    public bool Active { get; private set; } = true;
    public bool Used { get; private set; } = false;

    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    public Voucher(string code, VoucherDiscountType discountType, decimal discountValue,
        int quantity, DateTime validityDate)
    {
        AssertionConcern.AssertArgumentGreaterThanZero(discountValue,
            $"{discountValue} must be grater than zero");

        if (discountType == VoucherDiscountType.Percentage)
        {
            AssertionConcern.AssertArgumentMaximumValue(discountValue, 100.0m,
                $"the discount value of a percentage-type voucher cannot be greater than 100");
        }

        Code = code;
        DiscountType = discountType;
        DiscountValue = discountValue;
        Quantity = quantity;
        ValidityDate = validityDate;
    }

    public void UpdateVoucherUsageData()
    {
        if(Quantity > 0)
        {
            Quantity--;
        }
        else
        {
            Used = true;
        }
    }

    public DomainValidationResult IsApplicable()
    {
        List<string> errorMessages = [];

        var domainValidationResult = new DomainValidationResult()
        {
            IsValid = true,
            Messages = []
        };

        if (ValidityDate < DateTime.Now)
        {
            errorMessages.Add("This voucher has expired");
        }

        if (!Active)
        {
            errorMessages.Add("This voucher is no longer valid");
        }

        if (Used)
        {
            errorMessages.Add("This voucher has already been used");
        }

        if (Quantity == 0)
        {
            errorMessages.Add("This voucher is no longer available");
        }

        if (errorMessages.Count > 0)
        {
            domainValidationResult.IsValid = false;
            domainValidationResult.Messages = errorMessages;
        }

        return domainValidationResult;
    }
}
