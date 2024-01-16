using System;
using Memorization.Figure.ScriptableObjects;
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

        [Serializable]
        private class PatRecArguments
        {
            public int currentPatternSize;
            public int currentButtonNum;
            public int bestScore;
        }

        private PatRecArguments _arguments;

        private void Start()
        {
            _arguments = new PatRecArguments
            {
                currentPatternSize = patternLength.value,
                currentButtonNum = patternIndex.value+1,
                bestScore = bestScore.value,
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.currentPatternSize = patternLength.value;
            _arguments.currentButtonNum = patternIndex.value + 1;
            _arguments.bestScore = bestScore.value;

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.currentButtonNum = patternIndex.value + 1;
            _arguments.bestScore = bestScore.value;
            localizeStringEvent.RefreshString();
        }
    }
}