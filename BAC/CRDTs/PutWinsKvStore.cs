using BAC.CRDTs.Engines;
using BAC.CRDTs.Messages;
using BAC.CRDTs.Messages.Metadata;

namespace BAC.CRDTs;

public class PutWinsKvStore : ReplicatedKvStore<LamportClockOperation, LamportClockMetadata>
{
    public PutWinsKvStore(int nodeId) : base(new PutWinsKvStoreEngine(nodeId), nodeId)
    {
    }
}