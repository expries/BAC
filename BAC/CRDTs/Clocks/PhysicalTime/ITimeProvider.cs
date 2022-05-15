namespace BAC.CRDTs.Clocks.PhysicalTime;

public interface ITimeProvider
{
    public DateTime GetCurrentTime();
}