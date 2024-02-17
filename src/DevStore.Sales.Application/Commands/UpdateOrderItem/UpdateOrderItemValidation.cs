using DevStore.Sales.Domain;
using FluentValidation;

namespace DevStore.Sales.Application.Commands.UpdateOrderItem;

public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemValidation()
    {
        RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product id");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("The minimum quantity of an item is 1");

        RuleFor(c => c.Quantity)
            .LessThanOrEqualTo(Order.MaxOrderItemQuantity)
            .WithMessage("The maximum quantity of an item is 15");
    }
}
