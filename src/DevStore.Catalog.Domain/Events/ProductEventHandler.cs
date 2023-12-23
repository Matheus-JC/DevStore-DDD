using MediatR;

namespace DevStore.Catalog.Domain.Events;

public class ProductEventHandler(IProductRepository productRepository) : INotificationHandler<ProductBelowStockEvent>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(message.AggregateId);

        // TODO: send email to purchase more products
    }
}
