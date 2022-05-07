using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs.Messages;

public abstract class OperationBase<TMetadata> where TMetadata : MetadataBase
{
    public string Key { get; }
    
    public string? Value { get; }
    
    public OperationType Type { get; }
    
    public TMetadata Metadata { get;}

    protected OperationBase(string key, string value, TMetadata metadata)
    {
        Key = key;
        Value = value;
        Type = OperationType.Put;
        Metadata = metadata;
    }
    
    protected OperationBase(string key, TMetadata metadata)
    {
        Key = key;
        Value = null;
        Type = OperationType.Remove;
        Metadata = metadata;
    }
}