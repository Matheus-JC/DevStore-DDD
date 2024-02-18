namespace DevStore.Common.DomainObjects.DTOs.Order;

public class OrderProductsCollectionDTO
{
    public Guid OrderId { get; set; }
    public ICollection<OrderProductDTO> Products { get; set; } = [];
}
