using UnityEngine;
using Variables;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ThrowLevelRules", menuName = "ThrowAtTargets/ThrowLevelRules")]
    public class ThrowLevelRules : ScriptableObject
    {
        public IntVariable stagesVariable;
        public IntVariable targetsPerStageVariable;
        public IntVariable xSizeVariable;
        public IntVariable ySizeVariable;
        public FloatVariable chanceBigTargetVariable;
        public FloatVariable distBetweenStagesVariable;

        public int Stages
        {
            get => stagesVariable.value;
            set => stagesVariable.value = value;
        }

        public int TargetsPerStage
        {
            get => targetsPerStageVariable.value;
            set => targetsPerStageVariable.value = value;
        }

        public int XSize
        {
            get => xSizeVariable.value;
            set => xSizeVariable.value = value;
        }

        public int YSize
        {
            get => ySizeVariable.value;
            set => ySizeVariable.value = value;
        }

        public float ChanceBigTarget
        {
            get => chanceBigTargetVariable.value;
            set => chanceBigTargetVariable.value = value;
        }

        public float DistBetweenStages
        {
            get => distBetweenStagesVariable.value;
            set => distBetweenStagesVariable.value = value;
        }
    }
}