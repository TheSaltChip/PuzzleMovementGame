using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Serialization;
using Variables;

namespace UI.PatternRecognition
{
    public class PatternRecognitionGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private IntVariable bestScore;
        [SerializeField] private IntVariable patternIndex;
        [SerializeField] private IntVariable patternLength;
        [SerializeField] private BoolVariable isPatternCreated;

        [Serializable]
        private class PatRecArguments
        {
            public int currentPatternLength;
            public int buttonsLeft;
            public int bestScore;
            public bool isPatternCreated;
        }

        private PatRecArguments _arguments;

        private void Awake()
        {
            _arguments = new PatRecArguments
            {
                currentPatternLength = patternLength.value,
                buttonsLeft = patternLength.value - patternIndex.value,
                bestScore = bestScore.value,
                isPatternCreated = isPatternCreated.value
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.currentPatternLength = patternLength.value;
            _arguments.buttonsLeft = patternLength.value - patternIndex.value;
            _arguments.bestScore = bestScore.value;
            _arguments.isPatternCreated = isPatternCreated.value;

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.buttonsLeft = patternLength.value - patternIndex.value;
            _arguments.bestScore = bestScore.value;
            _arguments.isPatternCreated = isPatternCreated.value;

            localizeStringEvent.RefreshString();
        }

        public void UpdatePatternLength()
        {
            _arguments.currentPatternLength = patternLength.value;
            _arguments.buttonsLeft = patternLength.value - patternIndex.value;
            localizeStringEvent.RefreshString();
        }
    }
}