#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

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