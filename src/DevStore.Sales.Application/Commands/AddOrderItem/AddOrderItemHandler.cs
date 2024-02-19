using DevStore.Common.Communication.Mediator;
using DevStore.Sales.Application.Events.DraftOrderStarted;
using DevStore.Sales.Application.Events.OrderItemAdded;
using DevStore.Sales.Application.Events.OrderUpdated;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.AddOrderItem;

public class AddOrderItemHandler(
    IMediatorHandler mediatorHandler, 
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork), 
    IRequestHandler<AddOrderItemCommand, bool>
{
    public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);
        var ordemItem = new OrderItem(message.ProductId, message.ProductName, message.Quantity, message.UnitValue);

        if (order is null)
        {
            order = Order.OrderFactory.NewDraftOrder(message.ClientId);
            order.AddItem(ordemItem);

            _orderRepository.Add(order);
            order.AddEvent(new DraftOrderStartedEvent(order.Id, message.ClientId));
        }
        else
        {
            var orderExistingItem = order.OrderItemExists(ordemItem);
            order.AddItem(ordemItem);

            if (orderExistingItem)
            {
                _orderRepository.UpdateItem(order.GetExistingItem(ordemItem));
            }
            else
            {
                _orderRepository.AddItem(ordemItem);
            }
        }

        order.AddEvent(new OrderItemAddedEvent(order.Id, message.ClientId, message.ProductId, 
            message.ProductName, message.UnitValue, message.Quantity));

        return await _unitOfWork.Commit();
    }
}
