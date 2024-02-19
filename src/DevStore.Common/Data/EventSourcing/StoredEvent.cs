namespace DevStore.Common.Data.EventSourcing;

public class StoredEvent(Guid id, string tipo, DateTime ocurrenceDate, string data)
{
    public Guid Id { get; private set; } = id;
    public string Type { get; private set; } = tipo;
    public DateTime OccurrenceDate { get; private set; } = ocurrenceDate;
    public string Data { get; private set; } = data;
}
