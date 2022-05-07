﻿using BAC.CRDTs.Messages;

namespace BAC.CRDTs.Engines;

public class SiblingsKvStoreEngine : ICrdtEngine<LamportClockOperation>
{
    public Dictionary<string, LamportClockOperation> Operations { get; }
    
    public Dictionary<string, string> Values { get; }
    
    public LamportClockOperation PrepareUpdate(string key)
    {
        throw new NotImplementedException();
    }

    public LamportClockOperation PrepareUpdate(string key, string value)
    {
        throw new NotImplementedException();
    }

    public LamportClockOperation PrepareUpdateFromOtherReplica(LamportClockOperation operation)
    {
        throw new NotImplementedException();
    }

    public void EffectUpdate(LamportClockOperation operation)
    {
        throw new NotImplementedException();
    }
}