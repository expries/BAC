namespace BAC.CRDTs.Messages.Metadata;

public class VectorClockMetadata
{
    public string OperationId { get; set; }
    
    public int NodeId { get; set; }

    public Dictionary<int, int> Vector { get; set; }
    
    public VectorClockMetadata(string operationId, int nodeId, Dictionary<int, int> vector)
    {
        OperationId = operationId;
        NodeId = nodeId;
        Vector = vector;
    }
}