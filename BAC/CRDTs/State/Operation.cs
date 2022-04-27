using BAC.Events;

namespace BAC.CRDTs.State;

public class Operation
{
    public OperationType Type { get; }
    
    public LogicalClockEvent<string> ClockEvent { get; }
    
    public string? Value { get; }
    
    public Operation(OperationType type, LogicalClockEvent<string> clockEvent, string? value = null)
    {
        Type = type;
        ClockEvent = clockEvent;
        Value = value;
    }
}