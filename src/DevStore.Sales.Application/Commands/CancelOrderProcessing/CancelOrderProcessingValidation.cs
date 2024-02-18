using FluentValidation;

namespace DevStore.Sales.Application.Commands.CancelOrderProcessing;

public class CancelOrderProcessingValidation : AbstractValidator<CancelOrderProcessingCommand>
{
    public CancelOrderProcessingValidation()
    {
        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid order id");

        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client id");
    }
}
