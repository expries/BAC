using BAC.CRDTs.Messages;
using BAC.CRDTs.Replication;

namespace BAC.Interfaces;

public interface ICrdt
{
    public void Update(Operation operation);

    public bool PrepareUpdate(Operation operation);

    public void EffectUpdate(Operation operation);

    public Dictionary<string, string> GetValues();

    public List<Operation> GetOperations();
}