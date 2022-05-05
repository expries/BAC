using BAC.CRDTs.Interfaces;
using BAC.CRDTs.State;

namespace BAC.CRDTs.Engines;

public class VectorClockCrdtEngine : ICrdtEngine
{
    public void ApplyOperation(Operation operation)
    {
        throw new NotImplementedException();
    }

    public Operation? GetOperation(string key)
    {
        throw new NotImplementedException();
    }

    public List<Operation> GetOperations()
    {
        throw new NotImplementedException();
    }
}