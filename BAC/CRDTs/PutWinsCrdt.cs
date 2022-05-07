using BAC.CRDTs.Messages;

namespace BAC.CRDTs;

public class PutWinsCrdt
{
    private readonly Dictionary<string, LamportClockOperation> _operations = new();

    private readonly Dictionary<string, string> _values = new();

    public void Update(LamportClockOperation lamportClockOperation)
    {
        if (!PrepareUpdate(lamportClockOperation))
        {
            return;
        }
        
        _operations[lamportClockOperation.Key] = lamportClockOperation;
        EffectUpdate(lamportClockOperation);
    }

    public bool PrepareUpdate(LamportClockOperation lamportClockOperation)
    {
        // No conflicting operation
        if (!_operations.ContainsKey(lamportClockOperation.Key))
        {
            return true;
        }

        var storedOperation = _operations[lamportClockOperation.Key];

        // Happened before: keep stored operation
        if (lamportClockOperation.HappenedBefore(storedOperation))
        {
            return false;
        }

        // Happened after: use new operation
        if (!lamportClockOperation.IsConcurrentTo(storedOperation))
        {
            return true;
        }
        
        // If operations are concurrent, prefer PUT over REMOVE
        if (lamportClockOperation.Type != storedOperation.Type)
        {
            return lamportClockOperation.Type is not OperationType.Remove;
        }

        // Concurrent equal operations: Discard operation from node with lower node ID
        var useNewOperation = lamportClockOperation.Metadata.NodeId > storedOperation.Metadata.NodeId;
        return useNewOperation;
    }

    public void EffectUpdate(LamportClockOperation lamportClockOperation)
    {
        if (lamportClockOperation.Type is OperationType.Put && lamportClockOperation.Value is not null)
        {
            _values[lamportClockOperation.Key] = lamportClockOperation.Value;
            return;
        }

        _values.Remove(lamportClockOperation.Key);
    }

    public Dictionary<string, string> GetValues()
    {
        return _values;
    }

    public List<LamportClockOperation> GetOperations()
    {
        return _operations.Values.ToList();
    }
}