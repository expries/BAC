using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages.Operations;

/// <summary>
/// Operation carrying a vector clock
/// </summary>
public class VectorClockOperation : OperationBase<VectorClockMetadata>
{
    public VectorClockOperation(string key, string value, VectorClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public VectorClockOperation(string key, VectorClockMetadata metadata) : base(key, metadata)
    {
    }

    /// <summary>
    /// Returns whether this operation happened before a given other operation.
    /// Is equal to checking if the vector of this operation is smaller than the vector of the other operation.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool HappenedBefore(VectorClockOperation other)
    {
        foreach (var (nodeId, counter) in other.Metadata.Vector)
        {
            var localCounter = Metadata.Vector.ContainsKey(nodeId) ? Metadata.Vector[nodeId] : 0;
            if (localCounter > counter) return false;
        }

        return true;
    }
    
    /// <summary>
    /// Returns whether this operation happened after a given other operation.
    /// Is equal to checking if the vector of this operation is greater than the vector of the other operation.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool HappenedAfter(VectorClockOperation other)
    {
        foreach (var (nodeId, counter) in other.Metadata.Vector)
        {
            var localCounter = Metadata.Vector.ContainsKey(nodeId) ? Metadata.Vector[nodeId] : 0;
            if (localCounter < counter) return false;
        }

        return true;
    }
}