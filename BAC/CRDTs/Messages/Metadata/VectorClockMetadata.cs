namespace BAC.CRDTs.Messages.Metadata;

/// <summary>
/// Metadata that is attached to an operation carrying a vector clock
/// </summary>
public class VectorClockMetadata : MetadataBase
{
    public int NodeId { get; set; }

    public Dictionary<int, int> Vector { get; set; }
    
    public VectorClockMetadata(int nodeId, Dictionary<int, int> vector)
    {
        NodeId = nodeId;
        Vector = new Dictionary<int, int>();
        vector.ToList().ForEach(x => Vector[x.Key] = x.Value);
    }
}