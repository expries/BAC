using BAC.CRDTs.Messages;
using BAC.Interfaces;

namespace BAC.CRDTs;

public class LamportClockCrdt : ICrdt
{
    private readonly Dictionary<string, Operation> _operations = new();

    private readonly Dictionary<string, string> _values = new();

    public void Update(Operation operation)
    {
        if (!PrepareUpdate(operation))
        {
            return;
        }
        
        _operations[operation.Key] = operation;
        EffectUpdate(operation);
    }

    public bool PrepareUpdate(Operation operation)
    {
        // No conflicting operation
        if (!_operations.ContainsKey(operation.Key))
        {
            return true;
        }

        var storedOperation = _operations[operation.Key];

        // Happened before: keep stored operation
        if (operation.HappenedBefore(storedOperation))
        {
            return false;
        }

        // Happened after: use new operation
        if (!operation.IsConcurrentTo(storedOperation))
        {
            return true;
        }
        
        // If operations are concurrent, prefer PUT over REMOVE
        if (operation.Type != storedOperation.Type)
        {
            return operation.Type is not OperationType.Remove;
        }

        // Concurrent equal operations: Base ordering on event id
        var useNewOperation = operation.Metadata.OperationId.GetHashCode() > storedOperation.Metadata.OperationId.GetHashCode();
        return useNewOperation;
    }

    public void EffectUpdate(Operation operation)
    {
        if (operation.Type is OperationType.Put)
        {
            _values[operation.Key] = operation.Value;
            return;
        }

        _values.Remove(operation.Key);
    }

    public Dictionary<string, string> GetValues()
    {
        return _values;
    }

    public List<Operation> GetOperations()
    {
        return _operations.Values.ToList();
    }
}