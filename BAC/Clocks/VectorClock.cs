using BAC.CRDTs.Messages;

namespace BAC.Clocks;

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

    public void ReceiveMessage(LamportClockOperation operation)
    {
        var clockCounter = GetClockCounterByNodeId(operation.Metadata.NodeId);
        
        if (operation.Metadata.Counter > clockCounter)
        {
            Vector[operation.Metadata.NodeId] = operation.Metadata.Counter;
        }

        Increment();
    }

    public Dictionary<int, int> GetVector()
    {
        return Vector;
    }

    private int GetClockCounterByNodeId(int nodeId)
    {
        if (!Vector.ContainsKey(nodeId))
        {
            Vector[nodeId] = 0;
        }

        return Vector[nodeId];
    }
}