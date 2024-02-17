using DevStore.Catalog.Application.Services;
using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Commands.AddOrderItem;
using DevStore.Sales.Application.Commands.ApplyOrderVoucher;
using DevStore.Sales.Application.Commands.RemoveOrderItem;
using DevStore.Sales.Application.Commands.UpdateOrderItem;
using DevStore.Sales.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers;

public class ShoppingCartController(
    IProductAppService productAppService,
    INotificationHandler<DomainNotification> notifications, 
    IMediatorHandler mediatorHandler,
    IOrderQueries orderQueries) : MainController(notifications, mediatorHandler)
{
    private readonly IProductAppService _productAppService = productAppService;
    private readonly IOrderQueries _orderQueries = orderQueries;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    [Route("shopping-cart")]
    public async Task<IActionResult> Index()
    {
        return View(await _orderQueries.GetShoppingCartClient(ClientId));
    }

    [HttpPost("shopping-cart")]
    public async Task<IActionResult> AddItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetById(id);

        if (product == null)
            return BadRequest();

        if(product.Stock < quantity)
        {
            TempData["Error"] = "Product with insufficient stock";
            return RedirectToAction("ProductDetail", "Catalog", new { id });
        }

        var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, quantity, product.Price);
        await _mediatorHandler.SendCommand(command);

        if (IsOperatonValid())
        {
            return RedirectToAction("Index");
        }

        TempData["Errors"] = GetErrorMessages();
        return RedirectToAction("ProductDetail", "Catalog", new { id });
    }

    [HttpPost("remove-item")]
    public async Task<IActionResult> RemoveItem(Guid id)
    {
        var product = await _productAppService.GetById(id);

        if (product is null)
            return BadRequest();

        var command = new RemoveOrderItemCommand(ClientId, id);
        await _mediatorHandler.SendCommand(command);

        if (IsOperatonValid())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetShoppingCartClient(ClientId));
    }

    [HttpPost("update-item")]
    public async Task<IActionResult> UpdateItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetById(id);

        if (product is null)
            return BadRequest();

        var command = new UpdateOrderItemCommand(ClientId, id, quantity);
        await _mediatorHandler.SendCommand(command);

        if (IsOperatonValid())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetShoppingCartClient(ClientId));
    }

    [HttpPost("apply-voucher")]
    public async Task<IActionResult> ApplyVoucher(string voucherCode)
    {
        var command = new ApplyOrderVoucherCommand(ClientId, voucherCode);
        await _mediatorHandler.SendCommand(command);

        if (IsOperatonValid())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetShoppingCartClient(ClientId));
    }
}
