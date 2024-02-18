using FluentValidation;

namespace DevStore.Sales.Application.Commands.CancelOrderProcessingAndReverseStock;

public class CancelOrderProcessingAndReverseStockValidation : AbstractValidator<CancelOrderProcessingAndReverseStockCommand>
{
    public CancelOrderProcessingAndReverseStockValidation()
    {
        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid order id");

        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client id");
    }
}
