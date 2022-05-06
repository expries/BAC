using BAC.CRDTs.Messages;

namespace BAC.CRDTs.Replication;

public class ReplicationLog
{
    private readonly List<Operation> _operations = new();
    
    public void SetSafePoint(Operation operation)
    {
        _operations.Add(operation);
    }

    public bool SafePointIsAheadOfOperation(Operation operation)
    {
        return _operations.Any(x => x.Metadata.OperationId == operation.Metadata.OperationId);
    }
}