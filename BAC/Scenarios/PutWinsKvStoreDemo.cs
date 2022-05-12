using BAC.CRDTs;

namespace BAC.Scenarios;

/// <summary>
/// Sample scenario of a key-value store that is replicated twice and
/// uses a put-wins CRDT for conflict resolution
/// </summary>
public static class PutWinsKvStoreDemo
{
    public static void Show()
    {
        
        var kvA = new PutWinsKvStore(1);
        var kvB = new PutWinsKvStore(2);
        var kvC = new PutWinsKvStore(3);
        
        // A - Changes
        kvA.Put("a", "Milk!");
        kvA.Put("c", "Banana");
        kvA.Put("d", "Eggs");

        // B - Changes
        kvB.Put("a", "Chocolate");
        kvB.Remove("a");
        kvB.Put("b", "Lemon");

        // C - Sync to A
        kvC.Sync(kvA);
        
        // C - Changes
        kvC.Put("c", "Pear");
        
        kvA.Sync(kvB);
        kvB.Sync(kvA);
        
        kvB.Remove("d");
        
        kvA.Remove("b");
        
        kvB.Sync(kvA);
        kvB.Sync(kvC);
        
        kvC.Sync(kvB);
        kvC.Sync(kvA);
        
        kvA.Sync(kvC);
        kvB.Sync(kvC);

        PrintKeyValueStores(kvA, kvB, kvC);
    }

    private static void PrintKeyValueStores(PutWinsKvStore kvA, PutWinsKvStore kvB, PutWinsKvStore kvC)
    {
        Console.WriteLine("Keys for key-value-store A: ");
        Console.WriteLine("a: " + kvA.Get("a"));
        Console.WriteLine("b: " + kvA.Get("b"));
        Console.WriteLine("c: " + kvA.Get("c"));
        Console.WriteLine("d: " + kvA.Get("d"));

        Console.WriteLine("Keys for key-value-store B: ");
        Console.WriteLine("a: " + kvB.Get("a"));
        Console.WriteLine("b: " + kvB.Get("b"));
        Console.WriteLine("c: " + kvB.Get("c"));
        Console.WriteLine("d: " + kvB.Get("d"));
        
        Console.WriteLine("Keys for key-value-store C: ");
        Console.WriteLine("a: " + kvC.Get("a"));
        Console.WriteLine("b: " + kvC.Get("b"));
        Console.WriteLine("c: " + kvC.Get("c"));
        Console.WriteLine("d: " + kvC.Get("d"));
    }
}