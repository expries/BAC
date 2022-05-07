namespace BAC.CRDTs.Messages;

public abstract class OperationMessageBase
{
    public string Key { get; }
    
    public string? Value { get; }
    
    public OperationType Type { get; }

    protected OperationMessageBase(string key, string value)
    {
        Key = key;
        Value = value;
        Type = OperationType.Put;
    }
    
    protected OperationMessageBase(string key)
    {
        Key = key;
        Value = null;
        Type = OperationType.Remove;
    }
}