using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public class VectorClockOperation : OperationBase<VectorClockMetadata>
{
    public VectorClockOperation(string key, string value, VectorClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public VectorClockOperation(string key, VectorClockMetadata metadata) : base(key, metadata)
    {
    }

    public bool IsConcurrentTo(VectorClockOperation other)
    {
        throw new NotImplementedException();
    }

    public bool HappenedBefore(VectorClockOperation other)
    {
        throw new NotImplementedException();
    }
}