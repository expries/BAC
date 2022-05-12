using BAC.CRDTs.Messages.Operations;

namespace BAC.Clocks;

public class LamportClock
{
    public int Counter { get; private set; }

    public void Increment()
    {
        Counter++;
    }

    public void ReceiveMessage(LamportClockOperation operation)
    {
        if (operation.Metadata.Counter > Counter) Counter = operation.Metadata.Counter;
        Increment();
    }
}