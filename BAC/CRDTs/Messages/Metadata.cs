namespace BAC.CRDTs.Messages;

public class Metadata
{
    public string OperationId { get; set; }
    
    public int NodeId { get; set; }

    public int Counter { get; set; }
    
    public Metadata(string operationId, int nodeId, int counter)
    {
        OperationId = operationId;
        NodeId = nodeId;
        Counter = counter;
    }
}