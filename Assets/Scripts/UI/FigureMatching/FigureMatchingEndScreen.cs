using System;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace UI.FigureMatching
{
    public class FigureMatchingEndScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private FigureMatchingRules rules;

        [Serializable]
        private class Arguments
        {
            public int MaxNumShapes;
            public int MaxNumColor;
            public int NumToMatch;
            public int TotalNumberOfFigures;
        }

        private Arguments _arguments;

        private void Start()
        {
            _arguments = new Arguments
            {
                MaxNumShapes = rules.MaxNumShapes,
                MaxNumColor = rules.MaxNumColor,
                NumToMatch = rules.NumToMatch,
                TotalNumberOfFigures = rules.TotalTotalNumberOfFigures
            };

            localizeStringEvent.StringReference.Arguments = new object[]
                { _arguments };
        }

        public void LevelCompleted()
        {
            _arguments.MaxNumShapes = rules.MaxNumShapes;
            _arguments.MaxNumColor = rules.MaxNumColor;
            _arguments.NumToMatch = rules.NumToMatch;
            _arguments.TotalNumberOfFigures = rules.TotalTotalNumberOfFigures;
            
            localizeStringEvent.RefreshString();
        }
    };
}