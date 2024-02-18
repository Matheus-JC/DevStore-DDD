using DevStore.Catalog.Domain.Events;
using DevStore.Common.Communication.Mediator;
using DevStore.Common.DomainObjects.DTOs.Order;
using DevStore.Common.Messages.CommonMessages.Notifications;

namespace DevStore.Catalog.Domain;

public class StockService(
    IProductRepository productRepository,
    ICatalogUnitOfWork unitOfWork, 
    IMediatorHandler mediatorHandler) : IStockService
{
    public static int LowQuantityStock => 5;
    
    public readonly IProductRepository _productRepository = productRepository;
    public readonly ICatalogUnitOfWork _unitOfWork = unitOfWork;
    public readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var success = await DebitStockItem(productId, quantity);

        if (!success)
            return false;

        return await _unitOfWork.Commit();
    }

    public async Task<bool> DebitStockFromOrderProductsCollection(OrderProductsCollectionDTO products)
    {
        foreach (var item in products.Products)
        {
            var success = await DebitStockItem(item.Id, item.Quantity);

            if (!success)
                return false;
        }

        return await _unitOfWork.Commit();
    }

    private async Task<bool> DebitStockItem(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if (product is null)
            return false;

        if (!product.HasQuantityInStock(quantity))
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("Stock", 
                $"Product '{product.Name}' out of stock"));
            
            return false;
        }

        product.DebitStock(quantity);

        if (product.Stock < LowQuantityStock)
        {
            await _mediatorHandler.PublishEvent(new LowStockProductEvent(product.Id, product.Stock));
        }

        _productRepository.Update(product);

        return true;
    }

    public async Task<bool> ReplenishStock(Guid productId, int quantity)
    {
        var sucess = await ReplenishStockItem(productId, quantity);

        if (!sucess) return false;

        return await _unitOfWork.Commit();
    }

    public async Task<bool> ReplenishStockFromOrderProductsCollection(OrderProductsCollectionDTO products)
    {
        foreach (var item in products.Products)
        {
            await ReplenishStockItem(item.Id, item.Quantity);
        }

        return await _unitOfWork.Commit();
    }

    private async Task<bool> ReplenishStockItem(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if (product is null)
            return false;

        product.ReplenishStock(quantity);

        _productRepository.Update(product);

        return true;
    }

    public void Dispose()
    {
        _productRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
