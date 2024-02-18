namespace DevStore.Payment.AntiCorruption;

public class PayPalGateway : IPayPalGateway
{
    public bool CommitTransaction(string cardHashKey, string orderId, decimal amount)
    {
        return new Random().Next(2) == 0;
    }

    public string GetCardHashKey(string serviceKey, string creditCard)
    {
        return Util.GetRandomString();
    }

    public string GetPayPalServiceKey(string apiKey, string encriptionKey)
    {
        return Util.GetRandomString();
    }
}
