using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages.Operations;

/// <summary>
/// Operation carrying a physical clock timestamp
/// </summary>
public class PhysicalClockOperation : OperationBase<PhysicalClockMetadata>
{
    public PhysicalClockOperation(string key, string value, PhysicalClockMetadata metadata) : base(key, value, metadata)
    {
    }
    
    public PhysicalClockOperation(string key, PhysicalClockMetadata metadata) : base(key, metadata)
    {
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