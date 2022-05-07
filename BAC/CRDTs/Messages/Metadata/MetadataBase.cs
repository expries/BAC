namespace BAC.CRDTs.Messages.Metadata;

public class MetadataBase
{
    public string OperationId { get; set; }

    protected MetadataBase()
    {
        OperationId = Guid.NewGuid().ToString();
    }
}