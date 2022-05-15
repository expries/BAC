using BAC.CRDTs.Clocks.PhysicalTime;
using BAC.CRDTs.Interfaces;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs.Engines;

/// <summary>
/// A CRDT engine that on conflict preserves the value with the
/// most recent timestamp
/// </summary>
public class LastWriteWinsKvStoreEngine : ICrdtEngine<PhysicalClockOperation>
{
    private readonly ITimeProvider _timeProvider;
    
    private int NodeId { get; }
    
    public Dictionary<string, PhysicalClockOperation> Operations { get; } = new();
    
    public Dictionary<string, string> Values { get; } = new();
    
    public LastWriteWinsKvStoreEngine(int nodeId, ITimeProvider timeProvider)
    {
        NodeId = nodeId;
        _timeProvider = timeProvider;
    }
    
    public PhysicalClockOperation PrepareUpdate(string key)
    {
        var timestamp = _timeProvider.GetCurrentTime();
        var metadata = new PhysicalClockMetadata(NodeId, timestamp);
        return new PhysicalClockOperation(key, metadata);
    }

    public PhysicalClockOperation PrepareUpdate(string key, string value)
    {
        var timestamp = _timeProvider.GetCurrentTime();
        var metadata = new PhysicalClockMetadata(NodeId, timestamp);
        return new PhysicalClockOperation(key, value, metadata);
    }

    public PhysicalClockOperation ReceiveUpdate(PhysicalClockOperation operation)
    {
        operation.Metadata.OperationId = Guid.NewGuid().ToString();
        return operation;
    }

    public void EffectUpdate(PhysicalClockOperation operation)
    {
        var updateShouldBeApplied = UpdateShouldBeApplied(operation);

        if (!updateShouldBeApplied)
        {
            return;
        }
        
        if (operation.Type is OperationType.Put)
        {
            Values[operation.Key] = operation.Value ?? string.Empty;
        }
        else
        {
            Values.Remove(operation.Key);
        }

        Operations[operation.Key] = operation;
    }

    private bool UpdateShouldBeApplied(PhysicalClockOperation operation)
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
        if (operation.HappenedAfter(storedOperation))
        {
            return true;
        }

        // If operations have the same timestamp, use operation from node with higher node ID
        var useNewOperation = operation.Metadata.NodeId > storedOperation.Metadata.NodeId;
        return useNewOperation;
    }
}