namespace BAC.CRDTs.Messages.Metadata;

public class LamportClockMetadata
{
    public string OperationId { get; set; }
    
    public int NodeId { get; set; }

    public int Counter { get; set; }
    
    public LamportClockMetadata(string operationId, int nodeId, int counter)
    {
        OperationId = operationId;
        NodeId = nodeId;
        Counter = counter;
    }
}