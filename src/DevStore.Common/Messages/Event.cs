using MediatR;

namespace DevStore.Common.Messages;

public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; set; }

    protected Event() 
    {
        Timestamp = DateTime.Now;
    }
}
