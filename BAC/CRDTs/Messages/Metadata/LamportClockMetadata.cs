namespace BAC.CRDTs.Messages.Metadata;

public class LamportClockMetadata : MetadataBase
{

    public int Counter { get; set; }
    
    public LamportClockMetadata(int counter)
    {
        Counter = counter;
    }
}