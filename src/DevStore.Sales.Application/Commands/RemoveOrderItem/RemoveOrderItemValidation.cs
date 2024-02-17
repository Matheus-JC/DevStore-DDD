using FluentValidation;

namespace DevStore.Sales.Application.Commands.RemoveOrderItem;

public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
{
    public RemoveOrderItemValidation()
    {
        RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product id");
    }
}
