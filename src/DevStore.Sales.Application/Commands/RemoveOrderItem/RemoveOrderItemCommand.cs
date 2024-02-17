using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.RemoveOrderItem;

public class RemoveOrderItemCommand(Guid clientId, Guid productId) : Command
{
    public Guid ClientId { get; private set; } = clientId;
    public Guid ProductId { get; private set; } = productId;

    public override bool IsValid()
    {
        ValidationResult = new RemoveOrderItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}