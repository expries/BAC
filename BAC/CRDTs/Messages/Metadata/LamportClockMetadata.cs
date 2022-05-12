namespace BAC.CRDTs.Messages.Metadata;

/// <summary>
/// Metadata that is attached to an operation carrying a lamport clock timestamp
/// </summary>
public class LamportClockMetadata : MetadataBase
{
    public int Counter { get; set; }
    
    public LamportClockMetadata(int counter)
    {
        Counter = counter;
    }
}