using System;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;
using Variables;

namespace UI.FigureMatching
{
    public class FigureMatchingGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private IntVariable figuresLeft;

        [Serializable]
        private class Arguments
        {
            public int MaxNumShapes;
            public int MaxNumColor;
            public int NumToMatch;
            public int NumberOfFiguresLeft;
        }

        private Arguments _arguments;

        private void Start()
        {
            _arguments = new Arguments
            {
                MaxNumShapes = rules.MaxNumShapes,
                MaxNumColor = rules.MaxNumColor,
                NumToMatch = rules.NumToMatch,
                NumberOfFiguresLeft = figuresLeft.value
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.NumberOfFiguresLeft = figuresLeft.value;
            _arguments.MaxNumShapes = rules.MaxNumShapes;
            _arguments.MaxNumColor = rules.MaxNumColor;
            _arguments.NumToMatch = rules.NumToMatch;

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.NumberOfFiguresLeft = figuresLeft.value;
            localizeStringEvent.RefreshString();
        }
    }
}