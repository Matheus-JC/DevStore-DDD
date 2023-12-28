using DevStore.Catalog.Application.DTOs;
using DevStore.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers.Admin;

public class AdminProductsController(IProductAppService productAppService) : Controller
{
    public readonly IProductAppService _productAppService = productAppService;

    [HttpGet("admin-products")]
    public async Task<IActionResult> Index()
    {
        return View(await _productAppService.GetAll());
    }

    [HttpGet("new-product")]
    public async Task<IActionResult> NewProduct()
    {
        return View(await FillCategories(new ProductDTO()));
    }

    [HttpPost("new-product")]
    public async Task<IActionResult> NewProduct(ProductDTO productDTO)
    {
        if(!ModelState.IsValid)
            return View(await FillCategories(productDTO));

        await _productAppService.CreateProduct(productDTO);

        return RedirectToAction("Index");
    }

    [HttpGet("edit-product")]
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        var product = await _productAppService.GetById(id);

        if (product == null)
            return BadRequest("Unable to identify the product");

        return View(await FillCategories(product));
    }

    [HttpPost("edit-product")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductDTO productDTO)
    {
        var product = await _productAppService.GetById(id);
        productDTO.Stock = product.Stock;

        ModelState.Remove("Stock");

        if (!ModelState.IsValid) 
            return View(await FillCategories(productDTO));

        await _productAppService.UpdateProduct(productDTO);

        return RedirectToAction("Index");
    }

    [HttpGet("products-update-stock")]
    public async Task<IActionResult> UpdateStock(Guid id)
    {
        return View("Stock", await _productAppService.GetById(id));
    }

    [HttpPost("products-update-stock")]
    public async Task<IActionResult> UpdateStock(Guid id, int quantity)
    {
        if(quantity > 0)
            await _productAppService.ReplenishStock(id, quantity);
        else
            await _productAppService.DebitStock(id, quantity);

        return View("Index", await  _productAppService.GetAll());
    }

    private async Task<ProductDTO> FillCategories(ProductDTO product)
    {
        product.Categories = await _productAppService.GetCategories();
        return product;
    }
}
