using DevStore.Common.Messages;
using DevStore.Common.Messages.CommonMessages.Notifications;
using MediatR;

namespace DevStore.Common.Communication.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public readonly IMediator _mediator = mediator;

    public async Task<bool> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task PublishEvent<T>(T eventItem) where T : Event
    {
        await _mediator.Publish(eventItem);
    }

    public async Task PublishNotification<T>(T notification) where T : DomainNotification
    {
        await _mediator.Publish(notification);
    }
}
