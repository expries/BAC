using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.PutWinsCRDT;

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
        
        kv1.Put("a", "Milk!");
        kv1.Put("c", "Banana");
        kv2.Put("b", "Lemon");
        
        kv3.Sync(kv1);
        kv3.Put("c", "Pear");

        kv1.Sync(kv2);
        kv2.Sync(kv1);

        kv1.Remove("b");
        kv2.Sync(kv1);
        kv3.Sync(kv2);

        kv1.Sync(kv3);
        kv2.Sync(kv1);
        
        KvStorePrinter.Print(kv1, "N1", "a", "b", "c");
        KvStorePrinter.Print(kv2, "N2", "a", "b", "c");
        KvStorePrinter.Print(kv3, "N3", "a", "b", "c");
    }
}