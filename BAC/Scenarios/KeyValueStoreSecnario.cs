using BAC.CRDTs;

namespace BAC.Scenarios;

public static class KeyValueStoreScenario
{
    public static void Run()
    {
        var kvA = new KeyValueStore("kvA");
        var kvB = new KeyValueStore("kvB");
        
        kvA.Put("a", "Milk!");

        kvB.Put("a", "Chocolate");
        kvB.Remove("a");
        kvB.Put("b", "Lemon");
        
        kvA.Sync(kvB);
        kvB.Sync(kvA);

        kvA.Remove("b");
        kvB.Sync(kvA);

        Console.WriteLine(kvA.Get("a"));
        Console.WriteLine(kvA.Get("b"));

        Console.WriteLine(kvB.Get("a"));
        Console.WriteLine(kvB.Get("b"));
    }
}