using FluentValidation;

namespace DevStore.Sales.Application.Commands.StartOrder;

public class StartOrderValidation : AbstractValidator<StartOrderCommand>
{
    public StartOrderValidation()
    {
        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid order id");

        RuleFor(c => c.ClientId)
             .NotEqual(Guid.Empty)
             .WithMessage("Invalid client id");

        RuleFor(c => c.CardName)
            .NotEmpty()
            .WithMessage("The name on the card was not provided");

        RuleFor(c => c.CardNumber)
            .CreditCard()
            .WithMessage("Invalid credit card number");

        RuleFor(c => c.CardExpiration)
            .NotEmpty()
            .WithMessage("Expiration date not specified");

        RuleFor(c => c.CardCvv)
            .Length(3, 4)
            .WithMessage("The CVV was not filled out correctly");
    }
}
