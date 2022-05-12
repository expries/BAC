namespace BAC.CRDTs.Interfaces;

/// <summary>
/// Defines the functionality of a operation-based CRDT
/// </summary>
/// <typeparam name="TOperation"></typeparam>
public interface ICrdtEngine<TOperation>
{   
    /// <summary>
    /// Returns the last operation for every key
    /// </summary>
    public Dictionary<string, TOperation> Operations { get; }

    /// <summary>
    /// Returns the values of the key-value store
    /// </summary>
    public Dictionary<string, string> Values { get; }

    /// <summary>
    /// Returns a message that describes a remove update on the given key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TOperation PrepareUpdate(string key);

    /// <summary>
    /// Returns a message that describes a put update with given key and value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public TOperation PrepareUpdate(string key, string value);

    /// <summary>
    /// Returns a message that has been received by the CRDT
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public TOperation ReceiveUpdate(TOperation operation);

    /// <summary>
    /// Executes a change operation
    /// </summary>
    /// <param name="operation"></param>
    public void EffectUpdate(TOperation operation);
}