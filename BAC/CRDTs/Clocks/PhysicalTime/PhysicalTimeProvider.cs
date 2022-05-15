namespace BAC.CRDTs.Clocks.PhysicalTime;

public class PhysicalTimeProvider : ITimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}