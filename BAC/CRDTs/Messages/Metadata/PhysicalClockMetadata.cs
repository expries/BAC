namespace BAC.CRDTs.Messages.Metadata;

public class PhysicalClockMetadata : MetadataBase
{
    public int NodeId { get; set; }

    public DateTime Timestamp { get; set; }
    
    public PhysicalClockMetadata(int nodeId, DateTime timestamp)
    {
        NodeId = nodeId;
        Timestamp = timestamp;
    }
}