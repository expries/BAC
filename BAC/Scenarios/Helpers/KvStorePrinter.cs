using BAC.CRDTs.Interfaces;

namespace BAC.Scenarios.Helpers;

public static class KvStorePrinter
{
    public static void Print(IKeyValueStore kv, string id, params string[] keys)
    {
        Console.WriteLine($"Key-Value pairs for key-value-store {id}: ");
        foreach (var key in keys)
        {
            Console.WriteLine($"{key}: {kv.Get(key)}");
        }
    }
}