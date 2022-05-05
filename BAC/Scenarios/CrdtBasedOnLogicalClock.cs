using BAC.CRDTs;
using BAC.CRDTs.Engines;

namespace BAC.Scenarios;

public static class CrdtBasedOnLogicalClock
{
    public static void Show()
    {
        var engine = new LogicalClockCrdtEngine();
        var kvA = new KeyValueStore("kvA", engine);
        var kvB = new KeyValueStore("kvB", engine);
        var kvC = new KeyValueStore("kvC", engine);

        kvA.Put("c", "Banana");
        kvA.Put("a", "Milk!");

        kvB.Put("a", "Chocolate");
        kvB.Remove("a");
        
        kvB.Put("b", "Lemon");
        
        kvC.Sync(kvA);
        
        kvC.Put("c", "Pear");
        
        kvA.Sync(kvB);
        kvB.Sync(kvA);

        kvA.Remove("b");
        kvB.Sync(kvA);
        
        kvC.Sync(kvA);
        kvA.Sync(kvC);
        kvB.Sync(kvC);

        Console.WriteLine(kvA.Get("a"));
        Console.WriteLine(kvA.Get("b"));
        Console.WriteLine(kvA.Get("c"));

        Console.WriteLine(kvB.Get("a"));
        Console.WriteLine(kvB.Get("b"));
        Console.WriteLine(kvB.Get("c"));
        
        Console.WriteLine(kvC.Get("a"));
        Console.WriteLine(kvC.Get("b"));
        Console.WriteLine(kvC.Get("c"));
    }
}