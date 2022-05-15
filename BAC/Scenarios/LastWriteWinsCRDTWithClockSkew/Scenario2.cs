using BAC.CRDTs;
using BAC.CRDTs.Clocks.PhysicalTime;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.LastWriteWinsCRDTWithClockSkew;

public static class Scenario2
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 2");
        Console.WriteLine("-----------------------------------------");
        
        var clock = new PhysicalTimeProvider();
        var skewedClock = new SkewedClockProvider(TimeSpan.FromMilliseconds(3));
        
        var kv1 = new LastWriteWinsKvStore(1, clock);
        var kv2 = new LastWriteWinsKvStore(2, skewedClock);
        
        kv1.Put("a", "Milk");
        kv2.Put("a", "Chocolate");
        kv1.Remove("a");
        
        kv2.Sync(kv1);
        kv1.Sync(kv2);
        
        KvStorePrinter.Print(kv1, "N1", "a");
        KvStorePrinter.Print(kv2, "N2", "a");
    }
}