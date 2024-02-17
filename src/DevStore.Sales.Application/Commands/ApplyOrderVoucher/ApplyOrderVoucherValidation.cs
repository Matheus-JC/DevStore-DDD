using FluentValidation;

namespace DevStore.Sales.Application.Commands.ApplyOrderVoucher;

public class ApplyOrderVoucherValidation : AbstractValidator<ApplyOrderVoucherCommand>
{
    public ApplyOrderVoucherValidation()
    {
        RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");

        RuleFor(c => c.VoucherCode)
            .NotEmpty()
            .WithMessage("The voucher code cannot be empty");
    }
}
