using UnityEngine;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StageLocations", menuName = "Target/StageLocations")]
    public class StageLocations : ScriptableObject
    {
        public Vector3[] locations;
        public Vector3[] eulerRotations;
        
    }
}