using BAC.CRDTs.Messages;
using BAC.CRDTs.Replication;
using BAC.Interfaces;

namespace BAC.CRDTs;

public class VectorClockCrdt : ICrdt
{
    public void Update(Operation operation)
    {
        throw new NotImplementedException();
    }

    public bool PrepareUpdate(Operation operation)
    {
        throw new NotImplementedException();
    }

    public void EffectUpdate(Operation operation)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetValues()
    {
        throw new NotImplementedException();
    }

    public List<Operation> GetOperations()
    {
        throw new NotImplementedException();
    }
}