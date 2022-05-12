using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages.Operations;

public class LamportClockOperation : OperationBase<LamportClockMetadata>
{
    public LamportClockOperation(string key, string value, LamportClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public LamportClockOperation(string key, LamportClockMetadata metadata) : base(key, metadata)
    {
    }
}