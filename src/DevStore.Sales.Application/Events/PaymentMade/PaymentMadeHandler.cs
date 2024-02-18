using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands.FinalizeOrder;
using MediatR;

namespace DevStore.Sales.Application.Events.PaymentMade;

public class PaymentMadeHandler(IMediatorHandler mediatorHandler) : INotificationHandler<PaymentMadeEvent>
{
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task Handle(PaymentMadeEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommand(new FinalizeOrderCommand(message.OrderId, message.ClientId));
    }
}
