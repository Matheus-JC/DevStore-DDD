using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.StartOrder;

public class StartOrderCommand(Guid orderId, Guid clientId, decimal totalValue, string cardName, 
    string cardNumber, string cardExpiration, string cardCvv) : Command
{
    public Guid OrderId { get; private set; } = orderId;
    public Guid ClientId { get; private set; } = clientId;
    public decimal TotalValue { get; private set; } = totalValue;
    public string CardName { get; private set; } = cardName;
    public string CardNumber { get; private set; } = cardNumber;
    public string CardExpiration { get; private set; } = cardExpiration;
    public string CardCvv { get; private set; } = cardCvv;

    public override bool IsValid()
    {
        ValidationResult = new StartOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
