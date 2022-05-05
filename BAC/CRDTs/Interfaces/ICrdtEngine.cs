using BAC.CRDTs.State;

namespace BAC.CRDTs.Interfaces;

public interface ICrdtEngine
{
    public void ApplyOperation(Operation operation);

    public Operation? GetOperation(string key);

    public List<Operation> GetOperations();
}