using BAC.Events;

namespace BAC.CRDTs.State;

public class Operation
{
    public string Key { get; }
    
    public string? Value { get; }
    
    public OperationType Type { get; }
    
    public LogicalClockEvent ClockEvent { get; set; }

    public Operation(LogicalClockEvent clockEvent, string key, string value)
    {
        Type = OperationType.Put;
        ClockEvent = clockEvent;
        Key = key;
        Value = value;
    }
    
    public Operation(LogicalClockEvent clockEvent, string key)
    {
        Type = OperationType.Remove;
        ClockEvent = clockEvent;
        Key = key;
        Value = null;
    }
}