namespace BAC.Events;

public class LogicalClockEvent
{
    public string Id { get; }
    
    public string Name { get; }

    public int Counter { get; }
    
    public LogicalClockEvent(string id, string name, int counter)
    {
        Id = id;
        Name = name;
        Counter = counter;
    }

    public bool IsConcurrentTo(LogicalClockEvent other)
    {
        return other.Counter == Counter && other.Id != Id;
    }

    public bool HappenedBefore(LogicalClockEvent other)
    {
        return Counter < other.Counter;
    }
}