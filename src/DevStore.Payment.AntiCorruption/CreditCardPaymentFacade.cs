using DevStore.Payment.Business;

namespace DevStore.Payment.AntiCorruption;

public class CreditCardPaymentFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager) : ICreditCardPaymentFacade
{
    private readonly IPayPalGateway _payPalGateway = payPalGateway;
    private readonly IConfigurationManager _configManager = configManager;

    public PaymentTransaction MakePayment(Order order, PaymentItem payment)
    {
        var apiKey = _configManager.GetValue("apiKey");
        var encriptionKey = _configManager.GetValue("encriptionKey");

        var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
        var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

        var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.TotalValue);

        // TODO: The payment gateway that should return the transaction object
        var transaction = new PaymentTransaction
        {
            OrderId = order.Id,
            TotalValue = order.TotalValue,
            PaymentId = payment.Id
        };

        if (paymentResult)
        {
            transaction.StatusTransaction = PaymentTransactionStatus.Paid;
            return transaction;
        }

        transaction.StatusTransaction = PaymentTransactionStatus.Refused;
        return transaction;
    }
}
