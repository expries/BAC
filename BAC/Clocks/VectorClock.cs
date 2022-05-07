using BAC.CRDTs.Messages.Metadata;

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

    public LamportClockMetadata CreateMessage()
    {
        Vector[_nodeId]++;
        var id = Guid.NewGuid().ToString();
        var clockEvent = new LamportClockMetadata(id, _nodeId, Vector[_nodeId]);
        return clockEvent;
    }

    public LamportClockMetadata CreateReceiveMessage(LamportClockMetadata clockLamportClockMetadata)
    {
        var clockCounter = GetClockCounterByNodeId(clockLamportClockMetadata.NodeId);
        if (clockLamportClockMetadata.Counter > clockCounter)
        {
            Vector[clockLamportClockMetadata.NodeId] = clockLamportClockMetadata.Counter;
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