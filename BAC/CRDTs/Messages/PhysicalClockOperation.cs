using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public class PhysicalClockOperation : OperationMessageBase
{
    public PhysicalClockMetadata Metadata { get; }

    public PhysicalClockOperation(string key, string value, int nodeId, DateTime timestamp) : base(key, value)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new PhysicalClockMetadata(operationId, nodeId, timestamp);
    }
    
    public PhysicalClockOperation(string key, int nodeId, DateTime timestamp) : base(key)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new PhysicalClockMetadata(operationId, nodeId, timestamp);
    }

    public bool HappenedBefore(PhysicalClockOperation other)
    {
        return Metadata.Timestamp < other.Metadata.Timestamp;
    }

    public bool HappenedAfter(PhysicalClockOperation other)
    {
        return Metadata.Timestamp > other.Metadata.Timestamp;
    }
}