namespace BAC.Interfaces;

public interface IKeyValueStore<TValue>
{
    public void Put(string key, TValue value);

    public TValue? Get(string key);

    public void Remove(string key);
}