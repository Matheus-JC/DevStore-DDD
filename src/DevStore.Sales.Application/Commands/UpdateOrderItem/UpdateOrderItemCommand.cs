using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.UpdateOrderItem;

public class UpdateOrderItemCommand(Guid clientId, Guid productId, int quantity) : Command
{
    public Guid ClientId { get; private set; } = clientId;
    public Guid ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;

    public override bool IsValid()
    {
        ValidationResult = new UpdateOrderItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
