using DevStore.Common.Messages;

namespace DevStore.Common.Communication;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T message) where T : Event;
}