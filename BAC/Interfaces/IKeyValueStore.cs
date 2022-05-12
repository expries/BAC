namespace BAC.Interfaces;

/// <summary>
/// Defines the functionality of key-value store
/// </summary>
public interface IKeyValueStore
{
    /// <summary>
    /// Writes a given value to a given key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Put(string key, string value);

    /// <summary>
    /// Returns the value for a given key or null if no value is stored for that key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? Get(string key);

    /// <summary>
    /// Removes a value given its key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key);
}