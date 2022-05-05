using BAC.Clocks;
using BAC.Events;

namespace BAC.Scenarios;

public static class ConcurrencyScenario
{
    public static void Run()
    {
        var a = new LogicalClock("a");
        var b = new LogicalClock("b");

        var a1 = a.CreateEvent();
        var a2 = a.CreateEvent();
        var b1 = b.CreateEvent();

        var b2 = b.CreateReceiveEvent(a2);

        var b3 = b.CreateEvent();
        var a3 = a.CreateEvent();
        var b4 = b.CreateEvent();

        var a4 = a.CreateReceiveEvent(b4);

        var b5 = b.CreateEvent();
    }
}