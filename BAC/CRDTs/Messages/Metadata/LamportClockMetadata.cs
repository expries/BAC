namespace BAC.CRDTs.Messages.Metadata;

public class LamportClockMetadata : MetadataBase
{
    public int NodeId { get; set; }

    public int Counter { get; set; }
    
    public LamportClockMetadata(int nodeId, int counter)
    {
        NodeId = nodeId;
        Counter = counter;
    }
}