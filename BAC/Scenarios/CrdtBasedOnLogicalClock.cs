﻿namespace BAC.Scenarios;

public static class CrdtBasedOnLogicalClock
{
    public static void Show()
    {
        
        var kvA = new KeyValueStore(1);
        var kvB = new KeyValueStore(2);
        var kvC = new KeyValueStore(3);
        
        // A - Changes
        kvA.Put("a", "Milk!");
        kvA.Put("c", "Banana");

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
        
        kvA.Remove("b");
        
        kvB.Sync(kvA);
        kvC.Sync(kvB);
        
        kvA.Sync(kvC);
        kvB.Sync(kvC);

        PrintKeyValueStores(kvA, kvB, kvC);
    }

    private static void PrintKeyValueStores(KeyValueStore kvA, KeyValueStore kvB, KeyValueStore kvC)
    {
        Console.WriteLine("Keys for key-value-store A: ");
        Console.WriteLine("a: " + kvA.Get("a"));
        Console.WriteLine("b: " + kvA.Get("b"));
        Console.WriteLine("c: " + kvA.Get("c"));

        Console.WriteLine("Keys for key-value-store B: ");
        Console.WriteLine("a: " + kvB.Get("a"));
        Console.WriteLine("b: " + kvB.Get("b"));
        Console.WriteLine("c: " + kvB.Get("c"));
        
        Console.WriteLine("Keys for key-value-store C: ");
        Console.WriteLine("a: " + kvC.Get("a"));
        Console.WriteLine("b: " + kvC.Get("b"));
        Console.WriteLine("c: " + kvC.Get("c"));
    }
}