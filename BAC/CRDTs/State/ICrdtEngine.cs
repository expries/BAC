namespace BAC.CRDTs.State;

public interface ICrdtEngine
{
    public void ApplyOperation(Operation operation);

    public Operation? GetOperation(string key);

    public Dictionary<string, Operation> GetOperations();
}