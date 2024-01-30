using UnityEngine;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ThrowLevelRules", menuName = "ThrowAtTargets/ThrowLevelRules")]
    public class ThrowLevelRules : ScriptableObject
    {
        public int stages;
        public int targetsPerStage;
        public int xSize;
        public int ySize;
        public float chanceBigTarget;
        public float distBetweenStages;
    }
}