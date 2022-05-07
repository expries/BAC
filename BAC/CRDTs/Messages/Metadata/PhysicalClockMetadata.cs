namespace BAC.CRDTs.Messages.Metadata;

public class PhysicalClockMetadata
{
    public string OperationId { get; set; }
    
    public int NodeId { get; set; }

    public DateTime Timestamp { get; set; }
    
    public PhysicalClockMetadata(string operationId, int nodeId, DateTime timestamp)
    {
        OperationId = operationId;
        NodeId = nodeId;
        Timestamp = timestamp;
    }
}