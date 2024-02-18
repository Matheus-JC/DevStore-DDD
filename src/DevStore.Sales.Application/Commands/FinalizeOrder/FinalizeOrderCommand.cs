using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.FinalizeOrder;

public class FinalizeOrderCommand(Guid orderId, Guid ClientId) : Command
{
    public Guid OrderId { get; private set; } = orderId;
    public Guid ClientId { get; private set; } = ClientId;

    public override bool IsValid()
    {
        ValidationResult = new FinalizeOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
