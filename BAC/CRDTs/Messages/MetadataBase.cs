namespace BAC.CRDTs.Messages;

/// <summary>
/// Basic structure of metadata that is attached to an operation
/// </summary>
public class MetadataBase
{
    public string OperationId { get; set; }

    protected MetadataBase()
    {
        OperationId = Guid.NewGuid().ToString();
    }
}