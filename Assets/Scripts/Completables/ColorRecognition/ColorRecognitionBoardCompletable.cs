using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completables.ColorRecognition
{
    public class ColorRecognitionBoardCompletable : Completable
    {
        [field: SerializeField] public bool CanRepeat { get; set; }
        [SerializeField] private int patternLength;
        [SerializeField] private float blinkDuration;

        private ColorButtonCompletable[] _buttons;
        private List<int> _pattern;
        private int _patternIndex;

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

        protected void OnEnable()
        {
            foreach (var completable in _buttons) completable.OnDone.AddListener(CheckCompletion);
        }

        protected void OnDisable()
        {
            foreach (var completable in _buttons) completable.OnDone.RemoveListener(CheckCompletion);
        }

        public void ResetValues()
        {
            _pattern?.Clear();

            OnDisable();

            _buttons = GetComponentsInChildren<ColorButtonCompletable>();
            _patternIndex = 0;

            OnEnable();
        }

        public void CreatePattern()
        {
            _pattern.Clear();
            if (CanRepeat)
            {
                CreateRepeatingPattern();
                return;
            }

            CreateNonRepeatingPattern();
        }

        private void CreateRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternLength; i++)
            {
                var num = Random.Range(0, count);
                _pattern.Add(num);
            }
        }

        private void CreateNonRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternLength; i++)
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


        private void CheckCompletion()
        {
            if (_pattern.Count == 0) return;

            for (var buttonIndex = 0; buttonIndex < _buttons.Length; buttonIndex++)
            {
                var currentButton = _buttons[buttonIndex];

                if (!currentButton.IsDone) continue;

                var nextButtonIndexInPattern = _pattern[_patternIndex];

                if (buttonIndex != nextButtonIndexInPattern)
                {
                    Failed();
                    return;
                }

                currentButton.ResetState();
                ++_patternIndex;

                var patternIsNotDone = _patternIndex != _pattern.Count;

                if (patternIsNotDone) break;

                CompletedPattern();
                return;
            }

            OnIncompleteCheck.Invoke();
        }

        private void CompletedPattern()
        {
            _patternIndex = 0;
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
            patternLength = CanRepeat ? (int)val : (int)Mathf.Min(val, _buttons.Length);
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

            _patternIndex = 0;
        }
    }
}