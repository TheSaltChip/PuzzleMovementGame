using UnityEngine;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StageLocations", menuName = "Target/StageLocations")]
    public class StageLocationsSO : ScriptableObject
    {
        public PosRotScl[] posRots;
    }
}