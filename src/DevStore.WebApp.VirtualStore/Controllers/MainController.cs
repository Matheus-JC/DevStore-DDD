using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Controllers;

public abstract class MainController(
    INotificationHandler<DomainNotification> notifications, 
    IMediatorHandler mediatorHandler) : Controller
{
    private readonly DomainNotificationHandler _notifications = (DomainNotificationHandler) notifications;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

    // TODO: get logged client
    protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

    protected bool IsOperatonValid()
    {
        return !_notifications.AnyNotifications();
    }

    protected IEnumerable<string> GetErrorMessages()
    {
        return _notifications.GetNotifications().Select(c => c.Value).ToList();
    }

    protected void NotifyError(string code, string message)
    {
        _mediatorHandler.PublishNotification(new DomainNotification(code, message));
    }
}
