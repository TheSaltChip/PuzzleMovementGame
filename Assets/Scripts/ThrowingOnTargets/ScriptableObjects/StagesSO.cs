using UnityEngine;
using Variables;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TargetStages", menuName = "Target/Stages")]
    public class StagesSO : ScriptableObject
    {
        public IntReference currentStage;
        public StageLocationsSO[] stages;

        public StageLocationsSO CurrentStage()
        {
            return stages[Mathf.Clamp(currentStage.Value, 0, stages.Length-1)];
        }
    }
}