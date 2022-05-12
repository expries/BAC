namespace BAC.CRDTs.Engines;

public interface ICrdtEngine<TOperation>
{   
    public Dictionary<string, TOperation> Operations { get; }

    public Dictionary<string, string> Values { get; }

    public TOperation PrepareUpdate(string key);

    public TOperation PrepareUpdate(string key, string value);

    public TOperation ReceiveUpdate(TOperation operation);

    public void EffectUpdate(TOperation operation);
}