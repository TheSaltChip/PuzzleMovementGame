using UnityEngine;
using Variables;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TargetStages", menuName = "Target/Stages")]
    public class Stages : ScriptableObject
    {
        public IntReference currentStage;
        public StageLocations[] stages;

        public StageLocations CurrentStage()
        {
            return stages[Mathf.Clamp(currentStage.Value, 0, stages.Length-1)];
        }
    }
}