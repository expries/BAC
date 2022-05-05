using BAC.Clocks;
using BAC.CRDTs.Engines;
using BAC.CRDTs.Interfaces;
using BAC.CRDTs.State;

namespace BAC.CRDTs;

public class KeyValueStore : IKeyValueStore<string>
{
    private readonly ICrdtEngine _crdtEngine;

    private readonly Dictionary<string, ReplicationLog> _replicationLogs = new();

    private readonly LogicalClock _logicalClock;

    private string Identifier { get; }

    public KeyValueStore(string identifier, ICrdtEngine crdtEngine)
    {
        Identifier = identifier;
        _logicalClock = new LogicalClock(identifier);
        _replicationLogs[identifier] = new ReplicationLog();
        _crdtEngine = crdtEngine;
    }

    public void Put(string key, string value)
    {
        var clockEvent = _logicalClock.CreateEvent();
        var operation = new Operation(clockEvent, key, value);
        _crdtEngine.ApplyOperation(operation);
        SetSafePoint(operation);
    }

    public string? Get(string key)
    {
        var operation = _crdtEngine.GetOperation(key);
        return operation?.Type is OperationType.Put ? operation.Value : null;
    }

    public void Remove(string key)
    {
        if (Get(key) is null) return;
        
        var clockEvent = _logicalClock.CreateEvent();
        var operation = new Operation(clockEvent, key);
        _crdtEngine.ApplyOperation(operation);
        SetSafePoint(operation);
    }
    
    public void Sync(KeyValueStore other)
    {
        var operations = other.GetOperations();
        if (!_replicationLogs.ContainsKey(other.Identifier)) _replicationLogs[other.Identifier] = new ReplicationLog();

        foreach (var operation in operations)
        {
            var replicationLog = _replicationLogs[other.Identifier];
            if (replicationLog.IsPastOperation(operation) || IsPastOperation(operation)) continue;

            operation.ClockEvent = _logicalClock.CreateReceiveEvent(operation.ClockEvent);
            _crdtEngine.ApplyOperation(operation);
            replicationLog.SetSafePoint(operation);
            SetSafePoint(operation);
        }
    }

    private List<Operation> GetOperations()
    {
        return _crdtEngine.GetOperations();
    }
    
    private void SetSafePoint(Operation operation)
    {
        var replicationLog = _replicationLogs[Identifier];
        replicationLog.SetSafePoint(operation);
    }

    private bool IsPastOperation(Operation operation)
    {
        var replicationLog = _replicationLogs[Identifier];
        return replicationLog.IsPastOperation(operation);
    }
}