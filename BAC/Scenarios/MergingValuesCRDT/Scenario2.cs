using BAC.CRDTs;
using BAC.Scenarios.Helpers;

namespace BAC.Scenarios.MergingValuesCRDT;

public static class Scenario2
{
    public static void Show()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine(" Scenario 2");
        Console.WriteLine("-----------------------------------------");
        
        var kv1 = new MergingKvStore(1);
        var kv2 = new MergingKvStore(2);

        kv1.Put("a", "Milk");
        kv2.Put("a", "Chocolate");
        kv1.Remove("a");
        
        kv2.Sync(kv1);
        kv1.Sync(kv2);
        
        KvStorePrinter.Print(kv1, "N1", "a");
        KvStorePrinter.Print(kv2, "N2", "a");
    }
}