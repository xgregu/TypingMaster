namespace TypingMaster.Application.Events;

public abstract class EventBase
{
    public DateTime Timestamp { get; } = DateTime.Now;
}

public abstract record RecordEventBase
{
    public DateTime Timestamp { get; } = DateTime.Now;
}