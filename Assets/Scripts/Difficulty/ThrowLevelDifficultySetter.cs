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
                    CreateStages(1, 4, 0.75f, 4, 3);
                    break;
                case LevelDifficulty.Medium:
                    CreateStages(2, 6, 0.5f, 5, 4);
                    break;
                case LevelDifficulty.Hard:
                    CreateStages(3, 10, 0.3f, 5, 4);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
        }

        private void CreateStages(int numStages, int targetsPerStage, float chanceBig, int xSize, int ySize)
        {
            rules.Stages = numStages;
            rules.TargetsPerStage = targetsPerStage;
            rules.ChanceBigTarget = chanceBig;
            rules.XSize = xSize;
            rules.YSize = ySize;
        }
    }
}