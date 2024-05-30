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

using System.Collections;
using System.Collections.Generic;
using Completables;
using PatternRecognition.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PatternRecognition
{
    public class PatternRecognitionBoardCompletable : Completable
    {
        [SerializeField] private PatternRecognitionRules patternRecognitionRules;
        [SerializeField] private float blinkDuration;

        public UnityEvent onPatternCreated;
        public UnityEvent onResetValues;

        private ColorButtonCompletable[] _buttons;
        private List<int> _pattern;

        private WaitForSeconds _waitForOneSecond;
        private WaitForSeconds _waitForTenMilliSeconds;
        private Coroutine _co;

        private void Awake()
        {
            _waitForOneSecond = new WaitForSeconds(1);
            _waitForTenMilliSeconds = new WaitForSeconds(0.1f);
            _pattern = new List<int>();
            _buttons = GetComponentsInChildren<ColorButtonCompletable>();
            _co = null;
            patternRecognitionRules.SetDefaultValues();
        }

        private void Start()
        {
            CreatePattern();
        }

        public void ResetValues()
        {
            _pattern?.Clear();

            _buttons = GetComponentsInChildren<ColorButtonCompletable>();
            patternRecognitionRules.PatternIndex = 0;
            patternRecognitionRules.BestScore = 0;
            patternRecognitionRules.IsPatternCreated = false;

            onResetValues?.Invoke();
        }

        public void CreatePattern()
        {
            _pattern.Clear();
            patternRecognitionRules.PatternIndex = 0;
            patternRecognitionRules.BestScore = 0;
            patternRecognitionRules.IsPatternCreated = true;

            if (patternRecognitionRules.CanRepeat)
            {
                CreateRepeatingPattern();
            }
            else
            {
                CreateNonRepeatingPattern();
            }

            onPatternCreated?.Invoke();
        }

        private void CreateRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternRecognitionRules.PatternLength; i++)
            {
                var num = Random.Range(0, count);
                _pattern.Add(num);
            }
        }

        private void CreateNonRepeatingPattern()
        {
            var count = _buttons.Length;
            if (patternRecognitionRules.PatternLength > count)
                patternRecognitionRules.PatternLength = count;

            for (var i = 0; i < patternRecognitionRules.PatternLength; i++)
            {
                if (_pattern.Count == _buttons.Length) return;

                while (true)
                {
                    var num = Random.Range(0, count);

                    if (_pattern.Contains(num)) continue;

                    _pattern.Add(num);
                    break;
                }
            }
        }

        public void CheckCompletion()
        {
            if (_pattern.Count == 0) return;

            var patternIndex = patternRecognitionRules.PatternIndex;

            for (var buttonIndex = 0; buttonIndex < _buttons.Length; buttonIndex++)
            {
                var currentButton = _buttons[buttonIndex];

                if (!currentButton.IsDone) continue;

                var nextButtonIndexInPattern = _pattern[patternIndex];

                if (buttonIndex != nextButtonIndexInPattern)
                {
                    Failed();
                    return;
                }

                currentButton.ResetState();
                patternRecognitionRules.patternIndex.Increment();
                ++patternIndex;

                if (patternRecognitionRules.BestScore < patternIndex)
                {
                    patternRecognitionRules.BestScore = patternIndex;
                }

                var patternIsNotDone = patternIndex < _pattern.Count;

                if (patternIsNotDone) break;

                CompletedPattern();
                return;
            }

            OnIncompleteCheck.Invoke();
        }

        private void CompletedPattern()
        {
            patternRecognitionRules.PatternIndex = 0;
            BlinkCorrectButtons();

            if (patternRecognitionRules.ContinuousMode)
            {
                CreatePattern();
            }
            
            Completed();
        }

        private void Failed()
        {
            BlinkIncorrectButtons();
            ResetState();
            OnFailedCheck.Invoke();
        }

        public void BlinkPattern()
        {
            if (_co is not null)
                StopCoroutine(_co);
            _co = StartCoroutine(BlinkPatternCoroutine());
        }

        private IEnumerator BlinkPatternCoroutine()
        {
            yield return _waitForOneSecond;

            foreach (var index in _pattern)
            {
                yield return StartCoroutine(_buttons[index].Blink(blinkDuration));
                yield return _waitForTenMilliSeconds;
            }
        }

        private void BlinkCorrectButtons()
        {
            foreach (var colorButtonCompletable in _buttons)
                StartCoroutine(colorButtonCompletable.BlinkCorrectColor(blinkDuration));
        }

        private void BlinkIncorrectButtons()
        {
            foreach (var colorButtonCompletable in _buttons)
                StartCoroutine(colorButtonCompletable.BlinkIncorrectColor(blinkDuration));
        }

        public override void ResetState()
        {
            base.ResetState();

            foreach (var button in _buttons) button.ResetState();

            patternRecognitionRules.PatternIndex = 0;
        }
    }
}