using ThrowingOnTargets.Saveable;
using UnityEngine;
using Variables;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TargetStages", menuName = "ThrowAtTargets/Stages")]
    public class ThrowLevelSO : ScriptableObject
    {
        public string levelName;
        public IntReference currentStage;
        public Stage[] stages;

        public Stage CurrentStage()
        {
            return stages[Mathf.Clamp(currentStage.Value, 0, stages.Length-1)];
        }
    }
}