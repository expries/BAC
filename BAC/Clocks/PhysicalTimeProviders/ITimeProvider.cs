namespace BAC.Clocks.PhysicalTimeProviders;

public interface ITimeProvider
{
    public DateTime GetCurrentTime();
}