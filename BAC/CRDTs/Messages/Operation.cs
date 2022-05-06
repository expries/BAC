namespace BAC.CRDTs.Messages;

public class Operation
{
    public string Key { get; }
    
    public string? Value { get; }
    
    public OperationType Type { get; }
    
    public Metadata Metadata { get; }

    public Operation(string key, string value, int nodeId, int counter)
    {
        Key = key;
        Value = value;
        Type = OperationType.Put;

        var operationId = Guid.NewGuid().ToString();
        Metadata = new Metadata(operationId, nodeId, counter);
    }
    
    public Operation(string key, int nodeId, int counter)
    {
        Key = key;
        Value = null;
        Type = OperationType.Remove;
        
        var operationId = Guid.NewGuid().ToString();
        Metadata = new Metadata(operationId, nodeId, counter);
    }
    
    public bool IsConcurrentTo(Operation other)
    {
        return other.Metadata.Counter == Metadata.Counter && 
               other.Metadata.OperationId != Metadata.OperationId;
    }

    public bool HappenedBefore(Operation other)
    {
        return Metadata.Counter < other.Metadata.Counter;
    }
}