namespace BAC.Events;

public class LogicalClockEvent
{
    public string Id { get; }

    public int Counter { get; }
    
    public LogicalClockEvent(string id, int counter)
    {
        Counter = counter;
        Id = id;
    }

    public bool IsConcurrentTo(LogicalClockEvent other)
    {
        return other.Counter == Counter && other.Id != Id;
    }
}

public class LogicalClockEvent<TEventPayload>
{
    public string Id { get; }

    public int Counter { get; }
    
    public TEventPayload Payload { get; }
    
    public LogicalClockEvent(string id, int counter, TEventPayload payload)
    {
        Counter = counter;
        Id = id;
        Payload = payload;
    }

    public bool IsConcurrentTo(LogicalClockEvent<TEventPayload> other)
    {
        return other.Counter == Counter && other.Id != Id;
    }
}