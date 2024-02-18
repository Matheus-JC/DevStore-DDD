using DevStore.Common.DomainObjects.DTOs.Order;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace DevStore.Payment.Business.Events;

public class PaymentEventHandler(IPaymentService paymentService) : INotificationHandler<OrderStockConfirmedEvent>
{
    private readonly IPaymentService _paymentService = paymentService;

    public async Task Handle(OrderStockConfirmedEvent message, CancellationToken cancellationToken)
    {
        var orderPayment = new OrderPaymentDTO
        {
            OrderId = message.OrderId,
            ClientId = message.ClientId,
            TotalValue = message.TotalValue,
            CardName = message.CardName,
            CardNumber = message.CardNumber,
            CardExpiration = message.CardExpiration,
            CardCvv = message.CardCvv
        };

        await _paymentService.MakeOrderPayment(orderPayment);
    }
}
