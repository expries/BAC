namespace BAC.CRDTs.Messages.Metadata;

/// <summary>
/// Metadata that is attached to an operation carrying a physical clock timestamp
/// </summary>
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