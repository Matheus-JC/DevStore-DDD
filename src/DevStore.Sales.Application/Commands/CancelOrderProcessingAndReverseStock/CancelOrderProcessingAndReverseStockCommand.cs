using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.CancelOrderProcessingAndReverseStock;

public class CancelOrderProcessingAndReverseStockCommand(Guid orderId, Guid clientId) : Command
{
    public Guid OrderId { get; private set; } = orderId;
    public Guid ClientId { get; private set; } = clientId;

    public override bool IsValid()
    {
        ValidationResult = new CancelOrderProcessingAndReverseStockValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
