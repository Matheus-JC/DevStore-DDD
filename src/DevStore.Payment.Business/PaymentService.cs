using DevStore.Common.Communication.Mediator;
using DevStore.Common.DomainObjects.DTOs.Order;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Common.Messages.CommonMessages.Notifications;

namespace DevStore.Payment.Business;

public class PaymentService(
    ICreditCardPaymentFacade paymentCreditCardFacade, 
    IPaymentRepository paymentRepository, 
    IMediatorHandler mediatorHandler,
    IPaymentUnitOfWork unitOfWork) : IPaymentService
{
    private readonly ICreditCardPaymentFacade _paymentCreditCardFacade = paymentCreditCardFacade;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IPaymentUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task<PaymentTransaction> MakeOrderPayment(OrderPaymentDTO orderPayment)
    {
        var order = new Order
        {
            Id = orderPayment.OrderId,
            TotalValue = orderPayment.TotalValue,
        };

        var payment = new PaymentItem
        {
            OrderId = orderPayment.OrderId,
            TotalValue = orderPayment.TotalValue,
            CardName = orderPayment.CardName,
            CardNumber = orderPayment.CardNumber,
            CardExpiration = orderPayment.CardExpiration,
            CardCvv = orderPayment.CardCvv,
        };

        var transaction = _paymentCreditCardFacade.MakePayment(order, payment);

        if(transaction.StatusTransaction == PaymentTransactionStatus.Paid)
        {
            payment.AddEvent(new PaymentMadeEvent(order.Id, orderPayment.ClientId, transaction.PaymentId, transaction.Id, order.TotalValue));

            _paymentRepository.Add(payment);
            _paymentRepository.AddTransaction(transaction);

            await _unitOfWork.Commit();

            return transaction;
        }

        await _mediatorHandler.PublishNotification(new DomainNotification("payment", "The operator refused the payment"));
        await _mediatorHandler.PublishEvent(new PaymentRefusedEvent(order.Id, orderPayment.ClientId, transaction.PaymentId, transaction.Id, order.TotalValue));

        return transaction;
    }
}
