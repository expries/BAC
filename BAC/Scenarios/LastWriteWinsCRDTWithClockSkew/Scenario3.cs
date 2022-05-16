using BAC.CRDTs;
using BAC.CRDTs.Clocks.PhysicalTime;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.LastWriteWinsCRDTWithClockSkew;

public static class Scenario3
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 3 [LWW CRDT - with 3ms clock skew on N1]");
        Console.WriteLine("-----------------------------------------");

        var clock = new PhysicalTimeProvider();
        var skewedClock = new SkewedClockProvider(TimeSpan.FromMilliseconds(3));

        var kv1 = new LastWriteWinsKvStore(1, skewedClock);
        var kv2 = new LastWriteWinsKvStore(2, clock);
        var kv3 = new LastWriteWinsKvStore(3, clock);
        
        kv1.Put("b", "Lemon");
        kv2.Put("b", "Strawberry");
        kv1.Sync(kv2);
        kv2.Sync(kv1);
        kv3.Sync(kv2);
        
        KvStorePrinter.Print(kv1, "N1", "b");
        KvStorePrinter.Print(kv2, "N2", "b");
        KvStorePrinter.Print(kv3, "N3", "b");
    }
}