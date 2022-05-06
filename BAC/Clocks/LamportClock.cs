using BAC.CRDTs.Messages;

namespace BAC.Clocks;

public class LamportClock
{
    public int Counter { get; private set; }

    public void Increment()
    {
        Counter++;
    }

    public void ReceiveMessage(Operation operation)
    {
        if (operation.Metadata.Counter > Counter) Counter = operation.Metadata.Counter;
        Increment();
    }
}