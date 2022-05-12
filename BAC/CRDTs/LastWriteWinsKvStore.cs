using BAC.Clocks.PhysicalTimeProviders;
using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs;

/// <summary>
/// A key-values store that uses a Last-Write-Wins CRDT for conflict resolution
/// </summary>
public class LastWriteWinsKvStore : ReplicatedKvStore<PhysicalClockOperation, PhysicalClockMetadata>
{
    public LastWriteWinsKvStore(int nodeId, ITimeProvider timeProvider) 
        : base(new LastWriteWinsKvStoreEngine(nodeId, timeProvider), nodeId)
    {
    }
}