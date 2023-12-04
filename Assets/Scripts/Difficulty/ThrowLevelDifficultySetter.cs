using System;
using System.Collections.Generic;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Difficulty
{
    [CreateAssetMenu(fileName = "ThrowLevelDifficultySetter", menuName = "ThrowAtTargets/ThrowLevelDifficultySetter")]
    public class ThrowLevelDifficultySetter : AbstractDifficultyStrategy
    {
        [SerializeField] private ThrowLevelSO throwLevel;
        [SerializeField] private TargetInfo targetInfo;

        public override void SetDifficulty(LevelDifficulty difficulty)
        {
            throwLevel.levelName = difficulty.ToString();

            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    CreateStages(3, 4, 6);
                    break;
                case LevelDifficulty.Medium:
                    CreateStages(5, 6, 10);
                    break;
                case LevelDifficulty.Hard:
                    CreateStages(7, 10, 15);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
        }

        private void CreateStages(int numStages, int minTargetsPerStage, int maxTargetsPerStage)
        {
            var gridLength = targetInfo.grid.Length;
            var pos = new List<int>(numStages);
            throwLevel.stages = new Stage[numStages];

            for (var i = 0; i < numStages; i++)
            {
                var rand = Random.Range(minTargetsPerStage, maxTargetsPerStage + 1);

                while (true)
                {
                    if (pos.Count == gridLength || pos.Count == rand) break;

                    var num = Random.Range(0, gridLength);

                    if (pos.Contains(num)) continue;

                    pos.Add(num);
                }

                var stage = new Stage { posRots = new PosRotScl[pos.Count] };

                for (var j = 0; j < pos.Count; j++)
                {
                    stage.posRots[j] = new PosRotScl
                    {
                        location = targetInfo.grid[pos[j]],
                        rotation = new Vector3(90, 0, 180),
                        scale = Vector3.one
                    };
                }

                throwLevel.stages[i] = stage;
                throwLevel.currentStage.Value = 0;
                pos.Clear();
            }

            Array.Sort(throwLevel.stages, (s, w) => s.posRots.Length - w.posRots.Length);
        }
    }
}