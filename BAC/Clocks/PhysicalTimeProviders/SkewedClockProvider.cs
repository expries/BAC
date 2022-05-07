namespace BAC.Clocks.PhysicalTimeProviders;

public class SkewedClockProvider : ITimeProvider
{
    private readonly TimeSpan _clockSkew;
    
    public SkewedClockProvider(TimeSpan clockSkew)
    {
        _clockSkew = clockSkew;
    }
    
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow.Add(_clockSkew);
    }
}