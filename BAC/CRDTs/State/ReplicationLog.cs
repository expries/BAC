using BAC.Events;

namespace BAC.CRDTs.State;

public class ReplicationLog
{
    private readonly List<Operation> _operations = new();
    
    public void SetSafePoint(Operation operation)
    {
        _operations.Add(operation);
    }

    public bool IsPastOperation(Operation operation)
    {
        return _operations.Any(x => x.ClockEvent.Id == operation.ClockEvent.Id);
    }
}