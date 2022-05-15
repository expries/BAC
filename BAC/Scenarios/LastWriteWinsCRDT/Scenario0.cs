using BAC.CRDTs;
using BAC.CRDTs.Clocks.PhysicalTime;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.LastWriteWinsCRDT;

/// <summary>
/// Sample scenario of a key-value store that is replicated twice and
/// uses a last-write-wins CRDT for conflict resolution
/// </summary>
public static class Scenario0
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 0");
        Console.WriteLine("-----------------------------------------");
        
        var clock = new PhysicalTimeProvider();

        var kv1 = new LastWriteWinsKvStore(1, clock);
        var kv2 = new LastWriteWinsKvStore(2, clock);
        var kv3 = new LastWriteWinsKvStore(3, clock);
        
        kv1.Put("a", "Milk!");
        kv1.Put("c", "Banana");
        
        kv2.Put("b", "Lemon");
        
        kv3.Sync(kv1);
        
        kv3.Put("c", "Pear");

        kv1.Sync(kv2);
        kv2.Sync(kv1);

        kv1.Remove("b");
        
        kv2.Sync(kv1);
        kv1.Sync(kv2);
        
        kv3.Sync(kv2);
        kv3.Sync(kv1);
        
        kv1.Sync(kv3);
        kv2.Sync(kv3);
        
        KvStorePrinter.Print(kv1, "a", "a", "b", "c");
        KvStorePrinter.Print(kv2, "a", "a", "b", "c");
        KvStorePrinter.Print(kv3, "a", "a", "b", "c");
    }
}