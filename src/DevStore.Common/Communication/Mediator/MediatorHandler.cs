using DevStore.Common.Data.EventSourcing;
using DevStore.Common.Messages;
using DevStore.Common.Messages.CommonMessages.DomainEvents;
using DevStore.Common.Messages.CommonMessages.Notifications;
using MediatR;

namespace DevStore.Common.Communication.Mediator;

public class MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;
    private readonly IEventSourcingRepository _eventSourcingRepository = eventSourcingRepository;

    public async Task<bool> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task PublishEvent<T>(T eventItem) where T : Event
    {
        await _mediator.Publish(eventItem);
        await _eventSourcingRepository.SaveEvent(eventItem);
    }

    public async Task PublishNotification<T>(T notification) where T : DomainNotification
    {
        await _mediator.Publish(notification);
    }

    public async Task PublishDomainEvent<T>(T eventItem) where T : DomainEvent
    {
        await _mediator.Publish(eventItem);
    }
}
