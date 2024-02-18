using DevStore.Common.DomainObjects.DTOs.Order;

namespace DevStore.Common.Messages.CommonMessages.IntegrationEvents;

public class OrderProcessingCanceledEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public OrderProductsCollectionDTO OrderProdutcs { get; private set; }

    public OrderProcessingCanceledEvent(Guid orderId, Guid clientId, OrderProductsCollectionDTO orderProdutcs)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        OrderProdutcs = orderProdutcs;
    }
}
