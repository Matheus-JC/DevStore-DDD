using MediatR;

namespace DevStore.Common.Messages.CommonMessages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications = [];

    public Task Handle(DomainNotification message, CancellationToken cancellationToken)
    {
        _notifications.Add(message);
        return Task.CompletedTask;
    }

    public virtual List<DomainNotification> GetNotifications()
    {
        return _notifications;
    }

    public virtual bool AnyNotifications()
    {
        return GetNotifications().Count != 0;
    }

    public void Dispose()
    {
        _notifications = [];
    }
}
