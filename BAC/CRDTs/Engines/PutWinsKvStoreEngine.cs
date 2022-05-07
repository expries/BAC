using BAC.Clocks;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Engines;

public class PutWinsKvStoreEngine : ICrdtEngine<LamportClockOperation>
{

    private readonly LamportClock _lamportClock = new();
    
    private int NodeId { get; }

    public Dictionary<string, LamportClockOperation> Operations { get; } = new();

    public Dictionary<string, string> Values { get; } = new();
    
    public PutWinsKvStoreEngine(int nodeId)
    {
        NodeId = nodeId;
    }

    public LamportClockOperation PrepareUpdate(string key)
    {
        _lamportClock.Increment();
        var metadata = new LamportClockMetadata(NodeId, _lamportClock.Counter);
        return new LamportClockOperation(key, metadata);
    }
    
    public LamportClockOperation PrepareUpdate(string key, string value)
    {
        _lamportClock.Increment();
        var metadata = new LamportClockMetadata(NodeId, _lamportClock.Counter);
        return new LamportClockOperation(key, value, metadata);
    }

    public LamportClockOperation PrepareUpdateFromOtherReplica(LamportClockOperation operation)
    {
        _lamportClock.ReceiveMessage(operation);
        operation.Metadata.OperationId = Guid.NewGuid().ToString();
        operation.Metadata.NodeId = NodeId;
        operation.Metadata.Counter = _lamportClock.Counter;
        return operation;
    }

    public void EffectUpdate(LamportClockOperation operation)
    {
        var updateShouldBeApplied = UpdateShouldBeApplied(operation);

        if (!updateShouldBeApplied)
        {
            return;
        }
        
        if (operation.Type is OperationType.Put)
        {
            Values[operation.Key] = operation.Value;
        }
        else
        {
            Values.Remove(operation.Key);
        }

        Operations[operation.Key] = operation;
    }

    private bool UpdateShouldBeApplied(LamportClockOperation operation)
    {
        // No conflicting operation
        if (!Operations.ContainsKey(operation.Key))
        {
            return true;
        }

        var storedOperation = Operations[operation.Key];

        // operation happened before stored operation
        if (operation.HappenedBefore(storedOperation))
        {
            return false;
        }

        // operation happened after stored operation
        if (!operation.IsConcurrentTo(storedOperation))
        {
            return true;
        }
        
        // If operations are concurrent and of different type, prefer PUT over REMOVE
        if (operation.Type != storedOperation.Type)
        {
            return operation.Type is OperationType.Put;
        }
        
        // If operations are concurrent and of the same type, use operation from node with higher node ID
        var useNewOperation = operation.Metadata.NodeId > storedOperation.Metadata.NodeId;
        return useNewOperation;
    }
}