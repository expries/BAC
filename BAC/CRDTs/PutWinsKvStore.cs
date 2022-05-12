using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs;

/// <summary>
/// A key-values store that uses a Put-Wins CRDT for conflict resolution
/// </summary>
public class PutWinsKvStore : ReplicatedKvStore<VectorClockOperation, VectorClockMetadata>
{
    public PutWinsKvStore(int nodeId) : base(new PutWinsKvStoreEngine(nodeId), nodeId)
    {
    }
}