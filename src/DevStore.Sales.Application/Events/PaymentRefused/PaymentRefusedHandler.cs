using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands.CancelOrderProcessingAndReverseStock;
using MediatR;

namespace DevStore.Sales.Application.Events.PaymentRefused;

public class PaymentRefusedHandler(IMediatorHandler mediatorHandler) : INotificationHandler<PaymentRefusedEvent>
{
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task Handle(PaymentRefusedEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommand(new CancelOrderProcessingAndReverseStockCommand(message.OrderId, message.ClientId));
    }
}
