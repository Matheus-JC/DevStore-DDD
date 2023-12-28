using DevStore.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers;

public class CatalogController(IProductAppService productAppService) : Controller
{
    private readonly IProductAppService _productAppService = productAppService;

    [HttpGet]
    [Route("")]
    [Route("catalog")]
    public async Task<IActionResult> Index()
    {
        return View(await _productAppService.GetAll());
    }

    [HttpGet]
    [Route("product-detail/{id}")]
    public async Task<IActionResult> ProductDetail(Guid id)
    {
        return View(await _productAppService.GetById(id));
    }
}
