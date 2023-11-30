using System;
using System.Collections.Generic;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ThrowingOnTargets.Difficulty
{
    public class DifficultySetter : MonoBehaviour
    {
        [SerializeField] private ThrowLevelSO throwLevel;
        [SerializeField] private TargetInfo targetInfo;

        public void SetDifficulty(ThrowLevelDifficulty difficulty)
        {
            switch (difficulty)
            {
                case ThrowLevelDifficulty.Easy:
                    CreateStages(3, 4, 6);
                    break;
                case ThrowLevelDifficulty.Medium:
                    CreateStages(5, 6, 10);
                    break;
                case ThrowLevelDifficulty.Hard:
                    CreateStages(7, 8, 13);
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

                for (var j = 0; j < rand; j++)
                {
                    if (pos.Count == rand) return;

                    while (true)
                    {
                        var num = Random.Range(0, gridLength);

                        if (pos.Contains(num)) continue;

                        pos.Add(num);
                        break;
                    }
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
            }
        }

        public void SetEasyDifficulty()
        {
            SetDifficulty(ThrowLevelDifficulty.Easy);
        }
        
        public void SetMediumDifficulty()
        {
            SetDifficulty(ThrowLevelDifficulty.Medium);
        }
        
        public void SetHardDifficulty()
        {
            SetDifficulty(ThrowLevelDifficulty.Hard);
        }

        private const float LengthBetweenClosestTargets = 0.65f;

        // ReSharper disable InconsistentNaming
        private const float LengthBetweenCenterAnd2ndRing = 2 * LengthBetweenClosestTargets;
        private const float LengthBetweenCenterAnd3rdRing = 3 * LengthBetweenClosestTargets;
        // ReSharper restore InconsistentNaming

        // (x: cos(Length), y: sin(Length))

        /*
         * 1: 1
         * 2: 6 - 0 step turn
         * 3: 12 - 1 step turn
         * 4: 18 - 2 step turn
         * 5: 24 - 3 step turn
         *
         * 1   2    3    4
         * 7 - 19 - 37 - 61
         *   12   18   24
         *   2*6  3*6  4*6
         *
         * 1 + 6*1 | 1 + 6*1 + 6*2 | 1 + 6*1 + 6*2 + 6*3 | 1 + 6*1 + 6*2 + 6*3 + 6*4
         * 7              19               37                   61
         */
    }

    public enum ThrowLevelDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}