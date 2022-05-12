using BAC.CRDTs.Messages;

namespace BAC.CRDTs.Replication;

/// <summary>
/// Append-only log that stores replicated operations.
/// </summary>
/// <typeparam name="TOperation"></typeparam>
/// <typeparam name="TMetadata"></typeparam>
public class ReplicationLog<TOperation, TMetadata> 
    where TOperation : OperationBase<TMetadata> 
    where TMetadata : MetadataBase
{
    private readonly List<TOperation> _operations = new();
    
    /// <summary>
    /// Append an operation that was replicated
    /// </summary>
    /// <param name="operation"></param>
    public void Append(TOperation operation)
    {
        _operations.Add(operation);
    }

    /// <summary>
    /// Returns whether an operation was replicated
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public bool OperationWasReplicated(TOperation operation)
    {
        return _operations.Any(x => x.Metadata.OperationId == operation.Metadata.OperationId);
    }
}