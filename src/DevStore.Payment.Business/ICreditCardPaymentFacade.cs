
namespace DevStore.Payment.Business;

public interface ICreditCardPaymentFacade
{
    PaymentTransaction MakePayment(Order order, PaymentItem payment);
}
