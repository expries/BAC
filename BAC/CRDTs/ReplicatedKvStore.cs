﻿using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Replication;

namespace BAC.CRDTs;

public class ReplicatedKvStore<TOperation, TMetadata> 
    where TOperation : OperationBase<TMetadata> 
    where TMetadata : MetadataBase 
{
    protected readonly Dictionary<int, ReplicationLog<TMetadata>> ReplicationLogs = new();

    protected readonly ICrdtEngine<TOperation> Engine;

    protected int NodeId { get; }

    public ReplicatedKvStore(ICrdtEngine<TOperation> crdtEngine, int nodeId)
    {
        NodeId = nodeId;
        Engine = crdtEngine;
    }
    
    public string? Get(string key)
    {
        var values = Engine.Values;
        return values.ContainsKey(key) ? values[key] : null;
    }
    
    public void Put(string key, string value)
    {
        var log = GetReplicationLogByNodeId(NodeId);
        var operation = Engine.PrepareUpdate(key, value);
        Engine.EffectUpdate(operation);
        log.Append(operation);
    }

    public void Remove(string key)
    {
        var values = Engine.Values;
        
        if (!values.ContainsKey(key))
        {
            return;
        }
        
        var log = GetReplicationLogByNodeId(NodeId);
        var operation = Engine.PrepareUpdate(key);
        Engine.EffectUpdate(operation);
        log.Append(operation);
    }
    
    public void Sync(ReplicatedKvStore<TOperation, TMetadata> other)
    {
        var log = GetReplicationLogByNodeId(NodeId);
        var replicaLog = GetReplicationLogByNodeId(other.NodeId);
        other.Engine.Operations.Values.ToList().ForEach(x => SyncOperation(log, replicaLog, x));
    }

    private void SyncOperation(
        ReplicationLog<TMetadata> log, 
        ReplicationLog<TMetadata> replicaLog, 
        TOperation operation)
    {
        if (log.OperationWasApplied(operation) || replicaLog.OperationWasApplied(operation))
        {
            return;
        }

        var preparedOperation = Engine.PrepareUpdateFromOtherReplica(operation);
        Engine.EffectUpdate(preparedOperation);
        log.Append(preparedOperation);
        replicaLog.Append(preparedOperation);
    }
    
    private ReplicationLog<TMetadata> GetReplicationLogByNodeId(int nodeId)
    {
        if (!ReplicationLogs.ContainsKey(nodeId))
        {
            ReplicationLogs[nodeId] = new ReplicationLog<TMetadata>();
        }

        return ReplicationLogs[nodeId];
    }
}