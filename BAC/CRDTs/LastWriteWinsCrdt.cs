using BAC.CRDTs.Messages;

namespace BAC.CRDTs;

public class LastWriteWinsCrdt
{
    private readonly Dictionary<string, PhysicalClockOperation> _operations = new();

    private readonly Dictionary<string, string> _values = new();
    
    public void Update(PhysicalClockOperation operation)
    {
        throw new NotImplementedException();
    }

    public bool PrepareUpdate(PhysicalClockOperation operation)
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
        if (operation.HappenedAfter(storedOperation))
        {
            return true;
        }
        
        // Concurrent equal operations: Discard operation from node with lower node ID
        var useNewOperation = operation.Metadata.NodeId > storedOperation.Metadata.NodeId;
        return useNewOperation;
    }

    public void EffectUpdate(LamportClockOperation lamportClockOperation)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetValues()
    {
        throw new NotImplementedException();
    }

    public List<LamportClockOperation> GetOperations()
    {
        throw new NotImplementedException();
    }
}