using BAC.CRDTs;

namespace BAC.Scenarios;

public static class KeyValueStoreScenario
{
    public static void Run()
    {
        var kvA = new KeyValueStore();
        var kvB = new KeyValueStore();
        
        kvA.Put("a", "Milk!");

        kvB.Put("a", "Chocolate");
        kvB.Remove("a");
        kvB.Put("b", "Lemon");
        
        kvA.Sync(kvB);
        kvB.Sync(kvA);

        kvA.Remove("b");
        kvB.Sync(kvA);

        ;
    }
}