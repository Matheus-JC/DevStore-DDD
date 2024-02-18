using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands.CancelOrderProcessing;
using MediatR;

namespace DevStore.Sales.Application.Events.OrderStockRejected;

public class OrderStockRejectedHandler(IMediatorHandler mediatorHandler) : INotificationHandler<OrderStockRejectedEvent>
{
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommand(new CancelOrderProcessingCommand(message.OrderId, message.ClientId));
    }
}
