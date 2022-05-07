using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public class LamportClockOperation : OperationMessageBase
{
    public LamportClockMetadata Metadata { get; }

    public LamportClockOperation(string key, string value, int nodeId, int counter) : base(key, value)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new LamportClockMetadata(operationId, nodeId, counter);
    }
    
    public LamportClockOperation(string key, int nodeId, int counter) : base(key)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new LamportClockMetadata(operationId, nodeId, counter);
    }

    public bool IsConcurrentTo(LamportClockOperation other)
    {
        return other.Metadata.Counter == Metadata.Counter && 
               other.Metadata.OperationId != Metadata.OperationId;
    }

    public bool HappenedBefore(LamportClockOperation other)
    {
        return Metadata.Counter < other.Metadata.Counter;
    }
}