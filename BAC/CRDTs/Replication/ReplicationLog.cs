using BAC.CRDTs.Messages;

namespace BAC.CRDTs.Replication;

public class ReplicationLog
{
    private readonly List<LamportClockOperation> _operations = new();
    
    public void SetSafePoint(LamportClockOperation lamportClockOperation)
    {
        _operations.Add(lamportClockOperation);
    }

    public bool SafePointIsAheadOfOperation(LamportClockOperation lamportClockOperation)
    {
        return _operations.Any(x => x.Metadata.OperationId == lamportClockOperation.Metadata.OperationId);
    }
}