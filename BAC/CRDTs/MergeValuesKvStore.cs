using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs;

/// <summary>
/// A key-values store that uses a Put-Wins CRDT for conflict resolution
/// </summary>
public class MergingKvStore : ReplicatedKvStore<VectorClockOperation, VectorClockMetadata>
{
    public MergingKvStore(int nodeId) : base(new MergeValuesKvStoreEngine(nodeId), nodeId)
    {
    }
}