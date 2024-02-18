using DevStore.Common.Communication.Mediator;
using DevStore.Common.DomainObjects.DTOs.Order;
using DevStore.Common.Extensions;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.StartOrder;

public class StartOrderHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<StartOrderCommand, bool>
{
    public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

        if (order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
            return false;
        }

        order.StartOrder();

        var products = new List<OrderProductDTO>();

        order.OrderItems.ForEach(i => products.Add(
            new OrderProductDTO 
            {
                Id = i.ProductId,
                Quantity = i.Quantity
            })
        );

        var orderProductsCollection = new OrderProductsCollectionDTO { OrderId = order.Id, Products = products };

        order.AddEvent(new OrderStartedEvent(order.Id, order.ClientId, order.TotalValue, orderProductsCollection, 
            message.CardName, message.CardNumber, message.CardExpiration, message.CardCvv));

        _orderRepository.Update(order);

        return await _unitOfWork.Commit();
    }
}
