using BAC.CRDTs.Clocks;
using BAC.CRDTs.Interfaces;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs.Engines;

/// <summary>
/// A CRDT engine that prioritizes put over remove when handling concurrent writes.
/// Concurrency is detected using vector clocks.
/// </summary>
public class PutWinsKvStoreEngine : ICrdtEngine<VectorClockOperation>
{
    private readonly VectorClock _vectorClock;
    
    private int NodeId { get; }

    public Dictionary<string, VectorClockOperation> Operations { get; } = new();

    public Dictionary<string, string> Values { get; } = new();
    
    public PutWinsKvStoreEngine(int nodeId)
    {
        _vectorClock = new VectorClock(nodeId);
        NodeId = nodeId;
    }

    public VectorClockOperation PrepareUpdate(string key)
    {
        _vectorClock.Increment();
        var metadata = new VectorClockMetadata(NodeId, _vectorClock.Vector);
        return new VectorClockOperation(key, metadata);
    }
    
    public VectorClockOperation PrepareUpdate(string key, string value)
    {
        _vectorClock.Increment();
        var metadata = new VectorClockMetadata(NodeId, _vectorClock.Vector);
        return new VectorClockOperation(key, value, metadata);
    }

    public VectorClockOperation ReceiveUpdate(VectorClockOperation operation)
    {
        _vectorClock.ReceiveMessage(operation);
        operation.Metadata = new VectorClockMetadata(NodeId, _vectorClock.Vector);
        return operation;
    }

    public void EffectUpdate(VectorClockOperation operation)
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

    private bool UpdateShouldBeApplied(VectorClockOperation operation)
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