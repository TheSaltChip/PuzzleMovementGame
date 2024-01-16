using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Completables.ColorRecognition
{
    public class ColorRecognitionBoardCompletable : Completable
    {
        [SerializeField] private BoolVariable canRepeat;
        [SerializeField] private IntVariable patternLength;
        [SerializeField] private IntVariable patternIndex;
        [SerializeField] private IntVariable bestScore;
        [SerializeField] private float blinkDuration;

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
        }

        public void ResetValues()
        {
            _pattern?.Clear();
            
            _buttons = GetComponentsInChildren<ColorButtonCompletable>();
            patternIndex.value = 0;
            bestScore.value = 0;
        }

        public void CreatePattern()
        {
            _pattern.Clear();
            patternIndex.value = 0;
            bestScore.value = 0;
            
            if (canRepeat.value)
            {
                CreateRepeatingPattern();
                return;
            }

            CreateNonRepeatingPattern();
        }

        private void CreateRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternLength.value; i++)
            {
                var num = Random.Range(0, count);
                _pattern.Add(num);
            }
        }

        private void CreateNonRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternLength.value; i++)
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

            for (var buttonIndex = 0; buttonIndex < _buttons.Length; buttonIndex++)
            {
                var currentButton = _buttons[buttonIndex];

                if (!currentButton.IsDone) continue;

                var nextButtonIndexInPattern = _pattern[patternIndex.value];

                if (buttonIndex != nextButtonIndexInPattern)
                {
                    Failed();
                    return;
                }

                currentButton.ResetState();
                patternIndex.Increment();

                if (bestScore.value < patternIndex.value + 1)
                {
                    bestScore.value = patternIndex.value+1;
                }

                var patternIsNotDone = patternIndex.value != _pattern.Count;

                if (patternIsNotDone) break;

                CompletedPattern();
                return;
            }

            OnIncompleteCheck.Invoke();
        }

        private void CompletedPattern()
        {
            patternIndex.value = 0;
            BlinkCorrectButtons();

            Completed();
        }

        private void Failed()
        {
            BlinkIncorrectButtons();
            OnFailedCheck.Invoke();
            ResetState();
        }

        public void SetPatternLength(float val)
        {
            patternLength.value = canRepeat.value ? (int)val : (int)Mathf.Min(val, _buttons.Length);
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

            patternIndex.value = 0;
        }
    }
}