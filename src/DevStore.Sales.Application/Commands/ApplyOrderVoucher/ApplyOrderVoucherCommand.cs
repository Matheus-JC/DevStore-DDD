using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.ApplyOrderVoucher;

public class ApplyOrderVoucherCommand(Guid clientId, string voucherCode) : Command
{
    public Guid ClientId { get; private set; } = clientId;
    public string VoucherCode { get; private set; } = voucherCode;

    public override bool IsValid()
    {
        ValidationResult = new ApplyOrderVoucherValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
