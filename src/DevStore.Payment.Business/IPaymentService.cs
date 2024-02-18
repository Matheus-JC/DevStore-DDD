using DevStore.Common.DomainObjects.DTOs.Order;

namespace DevStore.Payment.Business;

public interface IPaymentService
{
    Task<PaymentTransaction> MakeOrderPayment(OrderPaymentDTO orderPayment);
}
