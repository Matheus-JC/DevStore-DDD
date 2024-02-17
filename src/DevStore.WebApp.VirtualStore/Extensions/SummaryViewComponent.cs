
using DevStore.Common.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Extensions;

public class SummaryViewComponent(INotificationHandler<DomainNotification> notifications) : ViewComponent
{
    private readonly DomainNotificationHandler _notifications = (DomainNotificationHandler) notifications;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var notificacoes = await Task.FromResult(_notifications.GetNotifications());
        notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

        return View();
    }
}
