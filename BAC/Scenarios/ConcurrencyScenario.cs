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

        b.CreateReceiveEvent(a2);

        var b3 = b.CreateEvent();
        var a3 = a.CreateEvent();
        var b4 = b.CreateEvent();

        a.CreateReceiveEvent(b4);

        var b5 = b.CreateEvent();

        var eventsA = a.GetEvents();
        var eventsB = b.GetEvents();

        var _events = new Dictionary<int, List<LogicalClockEvent>>();

        eventsA.ForEach(x =>
        {
            if (!_events.ContainsKey(x.Counter)) _events[x.Counter] = new List<LogicalClockEvent>();
            if (_events[x.Counter].All(y => y.Id != x.Id)) _events[x.Counter].Add(x);
        });

        eventsB.ForEach(x =>
        {
            if (!_events.ContainsKey(x.Counter)) _events[x.Counter] = new List<LogicalClockEvent>();
            if (_events[x.Counter].All(y => y.Id != x.Id)) _events[x.Counter].Add(x);
        });

        foreach (var (key, value) in _events.OrderBy(x => x.Key))
        {
            Console.WriteLine(key + ":");
            value.ForEach(x => Console.WriteLine(x.Id));
            Console.WriteLine();
        }

        var concurrentEvents = eventsB
            .Select(x => new KeyValuePair<LogicalClockEvent, List<LogicalClockEvent>>(x, a.GetConcurrentEvents(x)))
            .Where(x => x.Value.Count > 0)
            .ToList();

        ;
    }
}