namespace BAC.CRDTs.Clocks.PhysicalTimeProviders;

public interface ITimeProvider
{
    public DateTime GetCurrentTime();
}