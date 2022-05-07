using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Replication;

public class ReplicationLog<TMetadata> where TMetadata : MetadataBase
{
    private readonly List<OperationBase<TMetadata>> _operations = new();
    
    public void Append(OperationBase<TMetadata> operation)
    {
        _operations.Add(operation);
    }

    public bool OperationWasApplied(OperationBase<TMetadata> operation)
    {
        return _operations.Any(x => x.Metadata.OperationId == operation.Metadata.OperationId);
    }
}