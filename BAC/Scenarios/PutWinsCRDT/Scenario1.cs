using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.PutWinsCRDT;

public static class Scenario1
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 1");
        Console.WriteLine("-----------------------------------------");
        
        var kv1 = new PutWinsKvStore(1);
        var kv2 = new PutWinsKvStore(2);
        var kv3 = new PutWinsKvStore(3);
        
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