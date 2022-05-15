using BAC.CRDTs;
using BAC.CRDTs.Clocks.PhysicalTime;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.LastWriteWinsCRDTWithClockSkew;

public static class Scenario1
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 1 [LWW CRDT - with 3ms clock skew]");
        Console.WriteLine("-----------------------------------------");

        var clock = new PhysicalTimeProvider();
        var skewedClock = new SkewedClockProvider(TimeSpan.FromMilliseconds(3));
        
        var kv1 = new LastWriteWinsKvStore(1, skewedClock);
        var kv2 = new LastWriteWinsKvStore(2, clock);
        var kv3 = new LastWriteWinsKvStore(3, clock);
        
        kv1.Put("a", "Milk");
        
        kv2.Put("c", "Lemon");
        
        kv2.Sync(kv1);
        kv1.Sync(kv2);
        kv3.Sync(kv1);
        
        KvStorePrinter.Print(kv1, "N1", "a", "c");
        KvStorePrinter.Print(kv2, "N2", "a", "c");
        KvStorePrinter.Print(kv3, "N3", "a", "c");
    }
}