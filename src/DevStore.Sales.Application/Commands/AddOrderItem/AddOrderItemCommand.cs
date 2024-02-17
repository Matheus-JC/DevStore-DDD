using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Commands.AddOrderItem;

public class AddOrderItemCommand(Guid clientId, Guid productId, string productName,
    int quantity, decimal unitValue) : Command
{
    public Guid ClientId { get; private set; } = clientId;
    public Guid ProductId { get; private set; } = productId;
    public string ProductName { get; private set; } = productName;
    public int Quantity { get; private set; } = quantity;
    public decimal UnitValue { get; private set; } = unitValue;

    public override bool IsValid()
    {
        ValidationResult = new AddOrderItemValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
