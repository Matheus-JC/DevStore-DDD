using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Events.OrderFinalized;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.FinalizeOrder;

public class FinalizeOrderHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<FinalizeOrderCommand, bool>
{
    public async Task<bool> Handle(FinalizeOrderCommand message, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(message.OrderId);

        if(order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
            return false;
        }

        order.FinalizeOrder();

        order.AddEvent(new OrderFinalizedEvent(message.OrderId));
        return await _unitOfWork.Commit();
    }
}
