using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.PutWinsCRDT;

/// <summary>
/// Sample scenario of a key-value store that is replicated twice and
/// uses a put-wins CRDT for conflict resolution
/// </summary>
public static class Scenario0
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 0 [Put-Wins CRDT]");
        Console.WriteLine("-----------------------------------------");
        
        var kv1 = new PutWinsKvStore(1);
        var kv2 = new PutWinsKvStore(2);
        var kv3 = new PutWinsKvStore(3);
        
        // A - Changes
        kv1.Put("a", "Milk!");
        kv1.Put("c", "Banana");

        // B - Changes
        kv2.Put("b", "Lemon");
        
        // C - Sync to A
        kv3.Sync(kv1);
        
        // C - Changes
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