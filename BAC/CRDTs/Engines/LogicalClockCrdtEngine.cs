using BAC.CRDTs.Interfaces;
using BAC.CRDTs.State;

namespace BAC.CRDTs.Engines;

public class LogicalClockCrdtEngine : ICrdtEngine
{
    private readonly Dictionary<string, Operation> _operations = new();

    public void ApplyOperation(Operation operation)
    {
        // No conflicting operation
        if (!_operations.ContainsKey(operation.Key))
        {
            _operations[operation.Key] = operation;
            return;
        }

        var storedOperation = _operations[operation.Key];

        // Happened before: keep stored operation
        if (operation.ClockEvent.HappenedBefore(storedOperation.ClockEvent))
        {
            _operations[operation.Key] = storedOperation;
            return;
        }

        // Happened after: use new operation
        if (!operation.ClockEvent.IsConcurrentTo(storedOperation.ClockEvent))
        {
            _operations[operation.Key] = operation;
            return;
        }
        
        // Concurrent equal operations: Base ordering on event id
        if (operation.Type == storedOperation.Type)
        {
            _operations[operation.Key] = operation.ClockEvent.Id.GetHashCode() > storedOperation.ClockEvent.Id.GetHashCode() ? operation : storedOperation;
            return;
        }

        // Remove concurrent with a stored put: keep stored operation
        if (operation.Type == OperationType.Remove)
        {
            return;
        }
        
        // Put concurrent with a stored remove: use new operation
        _operations[operation.Key] = operation;
    }

    public Operation? GetOperation(string key)
    {
        return _operations.ContainsKey(key) ? _operations[key] : null;
    }

    public List<Operation> GetOperations()
    {
        return _operations.Values.ToList();
    }
}