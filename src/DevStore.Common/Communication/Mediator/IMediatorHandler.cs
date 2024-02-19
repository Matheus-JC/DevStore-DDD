using DevStore.Common.Messages;
using DevStore.Common.Messages.CommonMessages.DomainEvents;
using DevStore.Common.Messages.CommonMessages.Notifications;

namespace DevStore.Common.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventItem) where T : Event;
    Task<bool> SendCommand<T>(T command) where T : Command;
    Task PublishNotification<T>(T notification) where T : DomainNotification;
    Task PublishDomainEvent<T>(T eventItem) where T : DomainEvent;
}