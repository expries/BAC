using BAC.CRDTs.Messages;

namespace BAC.Clocks;

public class LamportClock
{
    public int Counter { get; private set; }

    public void Increment()
    {
        Counter++;
    }

    public void ReceiveMessage(LamportClockOperation lamportClockOperation)
    {
        if (lamportClockOperation.Metadata.Counter > Counter) Counter = lamportClockOperation.Metadata.Counter;
        Increment();
    }
}