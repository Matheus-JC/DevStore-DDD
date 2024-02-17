using DevStore.Catalog.Application.Services;
using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers;

public class CatalogController(
    IProductAppService productAppService,
    INotificationHandler<DomainNotification> notifications,
    IMediatorHandler mediatorHandler) : MainController(notifications, mediatorHandler)
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
