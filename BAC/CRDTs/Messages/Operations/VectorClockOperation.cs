using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages.Operations;

public class VectorClockOperation : OperationBase<VectorClockMetadata>
{
    public VectorClockOperation(string key, string value, VectorClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public VectorClockOperation(string key, VectorClockMetadata metadata) : base(key, metadata)
    {
    }

    public bool HappenedBefore(VectorClockOperation other)
    {
        foreach (var (nodeId, counter) in other.Metadata.Vector)
        {
            var localCounter = Metadata.Vector.ContainsKey(nodeId) ? Metadata.Vector[nodeId] : -1;
            if (localCounter > counter) return false;
        }

        return true;
    }
    
    public bool HappenedAfter(VectorClockOperation other)
    {
        foreach (var (nodeId, counter) in other.Metadata.Vector)
        {
            var localCounter = Metadata.Vector.ContainsKey(nodeId) ? Metadata.Vector[nodeId] : -1;
            if (localCounter < counter) return false;
        }

        return true;
    }
}