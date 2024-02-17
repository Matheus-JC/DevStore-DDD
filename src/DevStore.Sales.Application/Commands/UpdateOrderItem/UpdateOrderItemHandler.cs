using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Events.OrderItemRemoved;
using DevStore.Sales.Application.Events.OrderItemUpdated;
using DevStore.Sales.Application.Events.OrderUpdated;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.UpdateOrderItem;

public class UpdateOrderItemHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<UpdateOrderItemCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
    {
        if(!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

        if(order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
            return false;
        }

        var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

        if (orderItem is null || !order.OrderItemExists(orderItem))
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Item not found!"));
            return false;
        }

        order.UpdateUnits(orderItem, message.Quantity);

        order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));
        order.AddEvent(new OrderItemUpdatedEvent(order.Id, message.ClientId, message.ProductId, message.Quantity));

        _orderRepository.UpdateItem(orderItem);
        _orderRepository.Update(order);

        return await _unitOfWork.Commit();
    }
}
