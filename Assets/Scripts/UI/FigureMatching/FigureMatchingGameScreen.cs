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

        public void OnUpdateString()
        {
            _arguments.NumberOfFiguresLeft = figuresLeft.value;
            localizeStringEvent.RefreshString();
        }
    }
}