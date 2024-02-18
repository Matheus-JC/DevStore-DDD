using FluentValidation;

namespace DevStore.Sales.Application.Commands.FinalizeOrder;

public class FinalizeOrderValidation : AbstractValidator<FinalizeOrderCommand>
{
    public FinalizeOrderValidation()
    {
        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid order id");

        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client id");
    }
}
