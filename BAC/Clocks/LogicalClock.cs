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
        var clockEvent = new LogicalClockEvent(name, _counter);
        _events.Add(clockEvent);
        return clockEvent;
    }

    public void ReceiveEvent(LogicalClockEvent clockEvent)
    {
        if (clockEvent.Counter > _counter) _counter = clockEvent.Counter;
        CreateEvent();
    }

    public List<LogicalClockEvent> GetConcurrentEvents(LogicalClockEvent clockEvent)
    {
        return _events.Where(e => e.IsConcurrentTo(clockEvent)).ToList();
    }

    public List<LogicalClockEvent> GetEvents() => _events;
}

public class LogicalClock<TEventPayload> : AbstractClock
{
    private readonly List<LogicalClockEvent<TEventPayload>> _events = new();
    
    private int _counter;

    public LogicalClock(string name) : base(name)
    {
    }

    public LogicalClockEvent<TEventPayload> CreateEvent(TEventPayload value)
    {
        _counter++;
        var name = GenerateEventName();
        var clockEvent = new LogicalClockEvent<TEventPayload>(name, _counter, value);
        _events.Add(clockEvent);
        return clockEvent;
    }

    public void ReceiveEvent(LogicalClockEvent<TEventPayload> clockEvent)
    {
        if (_events.Any(@event => @event.Id == clockEvent.Id)) return;
        if (clockEvent.Counter > _counter) _counter = clockEvent.Counter;
        _counter++;
        _events.Add(clockEvent);
    }

    public List<LogicalClockEvent<TEventPayload>> GetConcurrentEvents(LogicalClockEvent<TEventPayload> clockEvent)
    {
        return _events.Where(@event => @event.IsConcurrentTo(clockEvent)).ToList();
    }
}