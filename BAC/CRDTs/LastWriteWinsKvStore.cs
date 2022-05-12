using BAC.Clocks.PhysicalTimeProviders;
using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs;

public class LastWriteWinsKvStore : ReplicatedKvStore<PhysicalClockOperation, PhysicalClockMetadata>
{
    public LastWriteWinsKvStore(int nodeId, ITimeProvider timeProvider) 
        : base(new LastWriteWinsKvStoreEngine(nodeId, timeProvider), nodeId)
    {
    }
}