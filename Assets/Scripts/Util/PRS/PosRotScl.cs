using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Util.PRS
{
    [Serializable]
    public struct PosRotScl
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }
}