using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

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
        bool test = Metadata.Timestamp < other.Metadata.Timestamp; 
        return Metadata.Timestamp < other.Metadata.Timestamp;
    }

    public bool HappenedAfter(PhysicalClockOperation other)
    {
        bool test = Metadata.Timestamp > other.Metadata.Timestamp;
        return Metadata.Timestamp > other.Metadata.Timestamp;
    }
}