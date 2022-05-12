using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs.Clocks;

public class VectorClock
{
    private readonly int _nodeId;

    public Dictionary<int, int> Vector { get; } = new();

    public VectorClock(int nodeId)
    {
        _nodeId = nodeId;
        Vector[nodeId] = 0;
    }

    public void Increment()
    {
        Vector[_nodeId]++;
    }

    public void ReceiveMessage(VectorClockOperation operation)
    {
        foreach (var (nodeId, counter) in operation.Metadata.Vector)
        {
            var localCounter = Vector.ContainsKey(nodeId) ? Vector[nodeId] : 0;
            Vector[nodeId] = Math.Max(counter, localCounter);
        }
        
        Increment();
    }
}