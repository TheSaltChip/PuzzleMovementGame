using System;
using UnityEngine;

namespace ThrowingOnTargets.ScriptableObjects
{
    [Serializable]
    public struct PosRotScl
    {
        public Vector3 location;
        public Vector3 rotation;
        public Vector3 scale;
    }
}