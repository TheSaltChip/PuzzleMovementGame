using System;
using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;
using Variables;

namespace UI.FigureMatching
{
    public class FigureMatchingGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private FloatVariable timeSpent;

        [Serializable]
        private class Arguments
        {
            public int NumToMatch;
            public int NumberOfFiguresLeft;
            public string TimeSpent;
        }

        private Arguments _arguments;

        private void Start()
        {
            _arguments = new Arguments
            {
                NumToMatch = rules.NumToMatch,
                NumberOfFiguresLeft = rules.NumFiguresLeft,
                TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff")
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.NumberOfFiguresLeft = rules.NumFiguresLeft;
            _arguments.NumToMatch = rules.NumToMatch;
            _arguments.TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff");

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.NumberOfFiguresLeft = rules.NumFiguresLeft;
            localizeStringEvent.RefreshString();
        }

        private void Update()
        {
            _arguments.TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff");
            localizeStringEvent.RefreshString();
        }
    }
}