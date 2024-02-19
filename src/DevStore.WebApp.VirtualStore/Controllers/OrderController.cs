using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DevStore.WebApp.VirtualStore.Controllers;

public class OrderController(
    INotificationHandler<DomainNotification> notifications,
    IMediatorHandler mediatorHandler,
    IOrderQueries orderQueries) : MainController(notifications, mediatorHandler)
{
    private readonly IOrderQueries _orderQueries = orderQueries;

    [HttpGet("order")]
    public async Task<IActionResult> Index()
    {
        return View(await _orderQueries.GetOrdersClient(ClientId));
    }
}
