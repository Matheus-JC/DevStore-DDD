using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace DevStore.Catalog.Domain.Events;

public class ProductEventHandler(
    IStockService stockService, 
    IMediatorHandler mediatorHandler
) : 
    INotificationHandler<LowStockProductEvent>,
    INotificationHandler<OrderStartedEvent>,
    INotificationHandler<OrderProcessingCanceledEvent>
{
    private readonly IStockService _stockService = stockService;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public Task Handle(LowStockProductEvent message, CancellationToken cancellationToken)
    {
        // TODO: send email reporting low stock
        return Task.CompletedTask;
    }

    public async Task Handle(OrderStartedEvent message, CancellationToken cancellationToken)
    {
        var success = await _stockService.DebitStockFromOrderProductsCollection(message.OrderProducts);

        if (success)
        {
            await _mediatorHandler.PublishEvent(new OrderStockConfirmedEvent(
                message.OrderId, message.ClientId, message.TotalValue, message.OrderProducts,
                message.CardName, message.CardNumber, message.CardExpiration, message.CardCvv));
        }
        else
        {
            await _mediatorHandler.PublishEvent(new OrderStockRejectedEvent(message.OrderId, message.ClientId));
        }
    }

    public async Task Handle(OrderProcessingCanceledEvent message, CancellationToken cancellationToken)
    {
        await _stockService.ReplenishStockFromOrderProductsCollection(message.OrderProdutcs);
    }
}
