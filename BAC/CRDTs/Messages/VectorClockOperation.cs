using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public class VectorClockOperation : OperationMessageBase
{
    public VectorClockMetadata Metadata { get; }

    public VectorClockOperation(string key, string value, int nodeId, Dictionary<int, int> vector) : base(key, value)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new VectorClockMetadata(operationId, nodeId, vector);
    }
    
    public VectorClockOperation(string key, int nodeId, Dictionary<int, int> vector) : base(key)
    {
        var operationId = Guid.NewGuid().ToString();
        Metadata = new VectorClockMetadata(operationId, nodeId, vector);
    }

    public bool IsConcurrentTo(VectorClockOperation other)
    {
        throw new NotImplementedException();
    }

    public bool HappenedBefore(VectorClockOperation other)
    {
        throw new NotImplementedException();
    }
}