using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages.Metadata;
using BAC.CRDTs.Messages.Operations;

namespace BAC.CRDTs;

public class PutWinsKvStore : ReplicatedKvStore<VectorClockOperation, VectorClockMetadata>
{
    public PutWinsKvStore(int nodeId) : base(new PutWinsKvStoreEngine(nodeId), nodeId)
    {
    }
}