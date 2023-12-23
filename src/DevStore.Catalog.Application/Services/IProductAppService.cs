using DevStore.Catalog.Application.DTOs;

namespace DevStore.Catalog.Application.Services;

public interface IProductAppService : IDisposable
{
    Task<IEnumerable<ProductDTO>> GetByCategory(int code);
    Task<ProductDTO> GetById(Guid id);
    Task<IEnumerable<ProductDTO>> GetAll();
    Task<IEnumerable<CategoryDTO>> GetCategories();

    Task CreateProduct(ProductDTO productDto);
    Task UpdateProduct(ProductDTO productDto);

    Task<ProductDTO> DebitStock(Guid id, int quantity);
    Task<ProductDTO> ReplenishStock(Guid id, int quantity);
}
