namespace BAC.CRDTs.Messages.Metadata;

public class VectorClockMetadata : MetadataBase
{
    public int NodeId { get; set; }

    public Dictionary<int, int> Vector { get; set; }
    
    public VectorClockMetadata(int nodeId, Dictionary<int, int> vector)
    {
        NodeId = nodeId;
        Vector = vector;
    }
}