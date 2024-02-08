using System;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;

namespace Difficulty
{
    [CreateAssetMenu(fileName = "ThrowLevelDifficultySetter", menuName = "ThrowAtTargets/ThrowLevelDifficultySetter")]
    public class ThrowLevelDifficultySetter : AbstractDifficultyStrategy
    {
        [SerializeField] private ThrowLevelRules rules;

        public override void SetDifficulty(LevelDifficulty difficulty)
        {
            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    CreateStages(1, 4, 4, 3, 0.75f,1f);
                    break;
                case LevelDifficulty.Medium:
                    CreateStages(2, 6, 5, 4, 0.5f, 1.5f);
                    break;
                case LevelDifficulty.Hard:
                    CreateStages(3, 10, 5, 4,0.3f,2f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
        }

        private void CreateStages(int numStages, int targetsPerStage, int xSize, int ySize, float chanceBig, float distanceBetween)
        {
            rules.Stages = numStages;
            rules.TargetsPerStage = targetsPerStage;
            rules.XSize = xSize;
            rules.YSize = ySize;
            rules.ChanceBigTarget = chanceBig;
            rules.DistBetweenStages = distanceBetween;
        }
    }
}