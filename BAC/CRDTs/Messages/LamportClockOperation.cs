using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public class LamportClockOperation : OperationBase<LamportClockMetadata>
{
    public LamportClockOperation(string key, string value, LamportClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public LamportClockOperation(string key, LamportClockMetadata metadata) : base(key, metadata)
    {
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