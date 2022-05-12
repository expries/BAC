using BAC.Clocks.PhysicalTimeProviders;
using BAC.CRDTs;

namespace BAC.Scenarios;

public static class LastWriteWinsKvStoreDemo
{
    public static void Show()
    {
        var clockSkew = TimeSpan.FromMilliseconds(0);
        var timeProvider = new PhysicalTimeProvider();
        var skewedProvider = new SkewedClockProvider(clockSkew);
        
        var kvA = new LastWriteWinsKvStore(1, skewedProvider);
        var kvB = new LastWriteWinsKvStore(2, timeProvider);
        var kvC = new LastWriteWinsKvStore(3, timeProvider);
        
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

    private static void PrintKeyValueStores(LastWriteWinsKvStore kvA, LastWriteWinsKvStore kvB, LastWriteWinsKvStore kvC)
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