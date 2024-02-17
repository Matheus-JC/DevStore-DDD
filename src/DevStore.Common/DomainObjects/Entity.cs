using DevStore.Common.Messages;

namespace DevStore.Common.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private readonly List<Event> _notifications = [];
    public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();

    public void AddEvent(Event eventItem)
    { 
        _notifications.Add(eventItem);
    }

    public void RemoveEvent(Event eventItem)
    {
        _notifications.Remove(eventItem);
    }

    public void ClearEvents()
    {
        _notifications.Clear();
    }

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) 
            return true;

        if (compareTo is null) 
            return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}
