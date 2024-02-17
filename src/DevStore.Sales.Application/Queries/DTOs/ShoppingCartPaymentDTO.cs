namespace DevStore.Sales.Application.Queries.DTOs;

public class ShoppingCartPaymentDTO
{
    public string CardName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string CardExpiration { get; set; } = string.Empty;
    public string CardCvv { get; set; } = string.Empty;
}
