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