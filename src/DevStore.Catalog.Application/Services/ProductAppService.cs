using AutoMapper;
using DevStore.Catalog.Application.DTOs;
using DevStore.Catalog.Domain;
using DevStore.Common.Data;
using DevStore.Common.DomainObjects;

namespace DevStore.Catalog.Application.Services;

public class ProductAppService(
    IProductRepository productRepository, 
    IStockService stockService, 
    IMapper mapper, 
    IUnitOfWork unitOfWork) : IProductAppService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IStockService _stockService = stockService;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<ProductDTO>> GetByCategory(int code)
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetByCategory(code));
    }

    public async Task<ProductDTO> GetById(Guid id)
    {
        return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
    }

    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAll());
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        return _mapper.Map<IEnumerable<CategoryDTO>>(await _productRepository.GetCategories());
    }

    public async Task CreateProduct(ProductDTO ProductDTO)
    {
        var product = _mapper.Map<Product>(ProductDTO);
        _productRepository.Create(product);

        await _unitOfWork.Commit();
    }

    public async Task UpdateProduct(ProductDTO ProductDTO)
    {
        var product = _mapper.Map<Product>(ProductDTO);
        _productRepository.Update(product);

        await _unitOfWork.Commit();
    }

    public async Task<ProductDTO> DebitStock(Guid id, int quantity)
    {
        if (!_stockService.DebitStock(id, quantity).Result)
        {
            throw new DomainException("failure to debit stock");
        }

        return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
    }

    public async Task<ProductDTO> ReplenishStock(Guid id, int quantity)
    {
        if (!_stockService.ReplenishStock(id, quantity).Result)
        {
            throw new DomainException("Failure to replenish stock");
        }

        return _mapper.Map<ProductDTO>(await _productRepository.GetById(id));
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
        _stockService?.Dispose();
        GC.SuppressFinalize(this);
    }
}
