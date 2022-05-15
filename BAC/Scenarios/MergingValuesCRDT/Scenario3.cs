using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.MergingValuesCRDT;

public static class Scenario3
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 3 [Merge-Values CRDT]");
        Console.WriteLine("-----------------------------------------");

        var kv1 = new MergingKvStore(1);
        var kv2 = new MergingKvStore(2);
        var kv3 = new MergingKvStore(3);
        
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