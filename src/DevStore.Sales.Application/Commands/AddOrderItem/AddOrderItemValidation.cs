using FluentValidation;

namespace DevStore.Sales.Application.Commands.AddOrderItem;

public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client id");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("invalid product id");

        RuleFor(c => c.ProductName)
            .NotEmpty()
            .WithMessage("The product name was not provided");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("The minimum quantity of an item is 1");

        RuleFor(c => c.Quantity)
            .LessThan(15)
            .WithMessage("The maximum quantity of an item is 15");

        RuleFor(c => c.UnitValue)
            .GreaterThan(0)
            .WithMessage("Item value must be greater than 0");
    }
}
