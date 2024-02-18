namespace DevStore.Common.Messages.CommonMessages.IntegrationEvents;

public class PaymentRefusedEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid PaymentId { get; private set; }
    public Guid TransactionId { get; private set; }
    public decimal TotalValue { get; private set; }

    public PaymentRefusedEvent(Guid orderId, Guid clientId, Guid paymentId, Guid transactionId, decimal totalValue)
    {
        AggregateId = paymentId;
        OrderId = orderId;
        ClientId = clientId;
        PaymentId = paymentId;
        TransactionId = transactionId;
        TotalValue = totalValue;
    }
}
