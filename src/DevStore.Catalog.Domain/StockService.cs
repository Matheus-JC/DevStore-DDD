using DevStore.Catalog.Domain.Events;
using DevStore.Common.Communication;
using DevStore.Common.Data;

namespace DevStore.Catalog.Domain;

public class StockService(
    IProductRepository productRepository, 
    IUnitOfWork unitOfWork, 
    IMediatorHandler mediatorHandler) : IStockService
{
    public static int LowQuantityStock => 10;
    
    public readonly IProductRepository _productRepository = productRepository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IMediatorHandler _mediatorHandler = mediatorHandler;


    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if (product == null)
            return false;

        if(!product.HasQuantityInStock(quantity)) 
            return false;

        product.DebitStock(quantity);

        if(product.Stock < LowQuantityStock)
        {
            await _mediatorHandler.PublishEvent(new ProductBelowStockEvent(product.Id, product.Stock));
        }

        _productRepository.Update(product);

        return await _unitOfWork.Commit();
    }

    public async Task<bool> ReplenishStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if (product == null) 
            return false;

        product.ReplenishStock(quantity);

        _productRepository.Update(product);

        return await _unitOfWork.Commit();
    }

    public void Dispose()
    {
        _productRepository.Dispose();
        GC.SuppressFinalize(this);
    }
}
