using BAC.Events;

namespace BAC.Clocks;

public class LogicalClock
{
    private int _counter;
    
    private string _name;
    
    private int _nameCounter;

    public LogicalClock(string name)
    {
        _name = name;
    }

    public LogicalClockEvent CreateEvent()
    {
        _counter++;
        var name = GenerateEventName();
        var id = Guid.NewGuid().ToString();
        var clockEvent = new LogicalClockEvent(id, name, _counter);
        return clockEvent;
    }

    public LogicalClockEvent CreateReceiveEvent(LogicalClockEvent clockEvent)
    {
        if (clockEvent.Counter > _counter) _counter = clockEvent.Counter;
        return CreateEvent();
    }

    private string GenerateEventName() => $"{_name}{++_nameCounter}";
}