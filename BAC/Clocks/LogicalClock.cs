using BAC.Events;

namespace BAC.Clocks;

public class LogicalClock : AbstractClock
{
    private readonly List<LogicalClockEvent> _events = new();
    
    private int _counter;

    public LogicalClock(string name) : base(name)
    {
    }

    public LogicalClockEvent CreateEvent()
    {
        _counter++;
        var name = GenerateEventName();
        var id = Guid.NewGuid().ToString();
        var clockEvent = new LogicalClockEvent(id, name, _counter);
        _events.Add(clockEvent);
        return clockEvent;
    }

    public LogicalClockEvent CreateReceiveEvent(LogicalClockEvent clockEvent)
    {
        if (clockEvent.Counter > _counter) _counter = clockEvent.Counter;
        return CreateEvent();
    }

    public List<LogicalClockEvent> GetConcurrentEvents(LogicalClockEvent clockEvent)
    {
        return _events.Where(e => e.IsConcurrentTo(clockEvent)).ToList();
    }

    public List<LogicalClockEvent> GetEvents() => _events;
}