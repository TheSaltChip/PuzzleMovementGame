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
using Difficulty;
using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UI.FigureMatching
{
    public class SetRulesFromDifficulty : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        public UnityEvent onDifficultySet;

        public void Set(LevelDifficulty difficulty)
        {
            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    SetEasy();
                    break;
                case LevelDifficulty.Medium:
                    SetMedium();
                    break;
                case LevelDifficulty.Hard:
                    SetHard();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }

            onDifficultySet?.Invoke();
        }

        private void SetEasy()
        {
            rules.NumToMatch = 2;
            rules.MaxNumColor = 3;
            rules.MaxNumShapes = 4;
            rules.TotalTotalNumberOfFigures = 12;
        }

        private void SetMedium()
        {
            rules.NumToMatch = 2;
            rules.MaxNumColor = 6;
            rules.MaxNumShapes = 10;
            rules.TotalTotalNumberOfFigures = 30;
        }

        private void SetHard()
        {
            rules.NumToMatch = 3;
            rules.MaxNumColor = 10;
            rules.MaxNumShapes = 16;
            rules.TotalTotalNumberOfFigures = 96;
        }
    }
}