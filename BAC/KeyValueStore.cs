using BAC.Clocks;
using BAC.CRDTs;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Replication;
using BAC.Interfaces;

namespace BAC;

public class KeyValueStore : IKeyValueStore<string>
{
    private readonly PutWinsCrdt _crdt;

    private readonly Dictionary<int, ReplicationLog> _replicationLogs = new();

    private readonly LamportClock _lamportClock = new();

    private int NodeId { get; }

    public KeyValueStore(int nodeId)
    {
        NodeId = nodeId;
        _replicationLogs[nodeId] = new ReplicationLog();
        _crdt = new PutWinsCrdt();
    }

    public string? Get(string key)
    {
        var values = _crdt.GetValues();
        return values.ContainsKey(key) ? values[key] : null;
    }
    
    public void Put(string key, string value)
    {
        _lamportClock.Increment();

        var putOperation = new LamportClockOperation(key, value, NodeId, _lamportClock.Counter);

        _crdt.Update(putOperation);

        var replicationLog = GetReplicationLogByNodeId(NodeId);
        replicationLog.SetSafePoint(putOperation);
    }

    public void Remove(string key)
    {
        if (Get(key) is null) return;
        
        _lamportClock.Increment();
        var removeOperation = new LamportClockOperation(key, NodeId, _lamportClock.Counter);
        
        _crdt.Update(removeOperation);
        
        var replicationLog = GetReplicationLogByNodeId(NodeId);
        replicationLog.SetSafePoint(removeOperation);
    }
    
    public void Sync(KeyValueStore other)
    {
        var localOperations = other.GetLocalOperations();
        var localReplicationLog = GetReplicationLogByNodeId(NodeId);
        var replicationLog = GetReplicationLogByNodeId(other.NodeId);
        
        foreach (var operation in localOperations)
        {
            // Do not apply the operation if it was applied locally
            if (replicationLog.SafePointIsAheadOfOperation(operation) || 
                localReplicationLog.SafePointIsAheadOfOperation(operation)) continue;
            
            _lamportClock.ReceiveMessage(operation);
            operation.Metadata.OperationId = Guid.NewGuid().ToString();
            operation.Metadata.NodeId = NodeId;
            operation.Metadata.Counter = _lamportClock.Counter;
            
            _crdt.Update(operation);
            
            replicationLog.SetSafePoint(operation);
            localReplicationLog.SetSafePoint(operation);
        }
    }
    
    /// <summary>
    /// Get all the local operations
    /// </summary>
    /// <returns></returns>
    private List<LamportClockOperation> GetLocalOperations()
    {
        return _crdt.GetOperations();
    }

    /// <summary>
    /// Returns the replication log for tracking the replicated operations of a node given its node ID
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    private ReplicationLog GetReplicationLogByNodeId(int nodeId)
    {
        if (!_replicationLogs.ContainsKey(nodeId)) _replicationLogs[nodeId] = new ReplicationLog();
        return _replicationLogs[nodeId];
    }
}