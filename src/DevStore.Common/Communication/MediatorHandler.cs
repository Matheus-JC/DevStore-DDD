using DevStore.Common.Messages;
using MediatR;

namespace DevStore.Common.Communication;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public readonly IMediator _mediator = mediator;

    public async Task PublishEvent<T>(T message) where T : Event
    {
        await _mediator.Publish(message);
    }
}
