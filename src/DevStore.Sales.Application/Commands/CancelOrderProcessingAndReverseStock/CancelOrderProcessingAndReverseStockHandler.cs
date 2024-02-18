using DevStore.Common.Communication.Mediator;
using DevStore.Common.DomainObjects.DTOs.Order;
using DevStore.Common.Extensions;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.Sales.Application.Commands.CancelOrderProcessingAndReverseStock;

public class CancelOrderProcessingAndReverseStockHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<CancelOrderProcessingAndReverseStockCommand, bool>
{
    public async Task<bool> Handle(CancelOrderProcessingAndReverseStockCommand message, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(message.OrderId);

        if(order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
            return false;
        }

        var products = new List<OrderProductDTO>();
        
        order.OrderItems.ForEach(i => products.Add(
            new OrderProductDTO
            {
                Id = i.ProductId,
                Quantity = i.Quantity,
            }
        ));

        var orderProductsCollection = new OrderProductsCollectionDTO
        {
            OrderId = order.Id,
            Products = products
        };

        order.AddEvent(new OrderProcessingCanceledEvent(order.Id, order.ClientId, orderProductsCollection));
        order.MakeDraft();

        return await _unitOfWork.Commit();
    }
}
