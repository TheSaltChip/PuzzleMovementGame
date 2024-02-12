using System;
using ThrowingOnTargets.ScriptableObjects;
using Util;
using Util.PRS;

namespace ThrowingOnTargets.Saveable
{
    [Serializable]
    public struct Stage
    {
        public PosRotScl[] posRots;
    }
}