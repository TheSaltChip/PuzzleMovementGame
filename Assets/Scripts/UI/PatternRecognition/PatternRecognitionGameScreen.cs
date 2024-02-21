using System;
using PatternRecognition.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace UI.PatternRecognition
{
    public class PatternRecognitionGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private PatternRecognitionRules rules;

        [Serializable]
        private class PatRecArguments
        {
            public int currentPatternLength;
            public int buttonsLeft;
            public int bestScore;
            public bool continuousMode;
        }

        private PatRecArguments _arguments;

        private void Awake()
        {
            _arguments = new PatRecArguments
            {
                currentPatternLength = rules.PatternLength,
                buttonsLeft = rules.PatternLength - rules.PatternIndex,
                bestScore = rules.BestScore,
                continuousMode = rules.ContinuousMode
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.currentPatternLength = rules.PatternLength;
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            _arguments.bestScore = rules.BestScore;
            _arguments.continuousMode = rules.ContinuousMode;

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            _arguments.bestScore = rules.BestScore;
            _arguments.continuousMode = rules.ContinuousMode;

            localizeStringEvent.RefreshString();
        }

        public void UpdatePatternLength()
        {
            _arguments.currentPatternLength = rules.PatternLength;
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            localizeStringEvent.RefreshString();
        }
    }
}