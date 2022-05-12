using BAC.CRDTs.Messages;

namespace BAC.CRDTs.Replication;

public class ReplicationLog<TOperation, TMetadata> 
    where TOperation : OperationBase<TMetadata> 
    where TMetadata : MetadataBase
{
    private readonly List<TOperation> _operations = new();
    
    public void Append(TOperation operation)
    {
        _operations.Add(operation);
    }

    public bool OperationWasApplied(TOperation operation)
    {
        return _operations.Any(x => x.Metadata.OperationId == operation.Metadata.OperationId);
    }
}