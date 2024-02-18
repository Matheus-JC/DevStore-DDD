using DevStore.Common.DomainObjects.DTOs.Order;

namespace DevStore.Common.Messages.CommonMessages.IntegrationEvents;

public class OrderStockConfirmedEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal TotalValue { get; private set; }
    public OrderProductsCollectionDTO OrderProducts { get; private set; }
    public string CardName { get; private set; }
    public string CardNumber { get; private set; }
    public string CardExpiration { get; private set; }
    public string CardCvv { get; private set; }

    public OrderStockConfirmedEvent(Guid orderId, Guid clientId, decimal totalValue, OrderProductsCollectionDTO products,
        string cardName, string cardNumber, string cardExpiration, string cardCvv)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        TotalValue = totalValue;
        OrderProducts = products;
        CardName = cardName;
        CardNumber = cardNumber;
        CardExpiration = cardExpiration;
        CardCvv = cardCvv;
    }
}
