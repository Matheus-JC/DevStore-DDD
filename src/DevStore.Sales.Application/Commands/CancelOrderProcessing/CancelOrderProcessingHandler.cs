using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.CancelOrderProcessing;

public class CancelOrderProcessingHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<CancelOrderProcessingCommand, bool>
{
    public async Task<bool> Handle(CancelOrderProcessingCommand message, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(message.OrderId);

        if (order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
            return false;
        }

        order.MakeDraft();

        return await _unitOfWork.Commit();
    }
}
