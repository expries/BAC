using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.PutWinsCRDT;

public static class Scenario3
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 3 [Put-Wins CRDT]");
        Console.WriteLine("-----------------------------------------");
        
        var kv1 = new PutWinsKvStore(1);
        var kv2 = new PutWinsKvStore(2);
        var kv3 = new PutWinsKvStore(3);
        
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