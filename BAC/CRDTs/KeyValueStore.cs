using BAC.Clocks;
using BAC.CRDTs.Interfaces;
using BAC.CRDTs.State;

namespace BAC.CRDTs;

public class KeyValueStore : IKeyValueStore<string>
{
    private readonly LogicalClock<string> _logicalClock = new(Guid.NewGuid().ToString());
    
    private readonly Dictionary<string, string> _store = new();

    private readonly Dictionary<string, Operation> _operations = new();

    public void Put(string key, string value)
    {
        var clockEvent = _logicalClock.CreateEvent(key);
        _operations[key] = new Operation(OperationType.Put, clockEvent, value);
        _store[key] = value;
    }

    public string? Get(string key)
    {
        return _store.ContainsKey(key) ? _store[key] : null;
    }

    public void Remove(string key)
    {
        if (!_store.ContainsKey(key)) return;
        var clockEvent = _logicalClock.CreateEvent(key);
        _operations[key] = new Operation(OperationType.Remove, clockEvent);
        _store.Remove(key);
    }
    
    public void Sync(KeyValueStore other)
    {
        var operations = other.GetOperations();
        operations.ToList().ForEach(kv => SyncOperation(kv.Key, kv.Value));
    }

    private Dictionary<string, Operation> GetOperations()
    {
        return _operations;
    }
    
    private void SyncOperation(string key, Operation operation)
    {
        _logicalClock.ReceiveEvent(operation.ClockEvent);
        var concurrentEvents = _logicalClock.GetConcurrentEvents(operation.ClockEvent);
        var conflictExists = concurrentEvents.Any(@event => @event.Payload == key);

        if (conflictExists)
        {
            ResolveConflict(key, operation);
            return;
        }

        ApplyOperation(key, operation);
    }

    private void ResolveConflict(string key, Operation concurrentOperation)
    {
        var localOperation = _operations[key];
            
        // Favour local operation over concurrent one
        if (localOperation.Type == concurrentOperation.Type)
        {
            return;
        }

        // Favour put over remove
        if (concurrentOperation.Type is OperationType.Put)
        {
            _store[key] = concurrentOperation.Value;
            _operations[key] = concurrentOperation;
        }
    }
    
    private void ApplyOperation(string key, Operation operation)
    {
        switch (operation.Type)
        {
            case OperationType.Put:
                _operations[key] = operation;
                _store[key] = operation.Value;
                return;
                    
            case OperationType.Remove:
                _operations[key] = operation;
                _store.Remove(key);
                return;
            
            default:
                return;
        }
    }
}