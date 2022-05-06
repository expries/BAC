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

    public Metadata CreateMessage()
    {
        Vector[_nodeId]++;
        var id = Guid.NewGuid().ToString();
        var clockEvent = new Metadata(id, _nodeId, Vector[_nodeId]);
        return clockEvent;
    }

    public Metadata CreateReceiveMessage(Metadata clockMetadata)
    {
        var clockCounter = GetClockCounterByNodeId(clockMetadata.NodeId);
        if (clockMetadata.Counter > clockCounter)
        {
            Vector[clockMetadata.NodeId] = clockMetadata.Counter;
        }

        return CreateMessage();
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