using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Completables.ColorRecognition
{
    public class ColorRecognitionBoardCompletable : Completable
    {
        [SerializeField] private bool canRepeat;
        [SerializeField] private int patternLength;
        [SerializeField] private float blinkDuration;

        private ColorButtonCompletable[] _buttons;
        private List<int> _pattern;
        private int _patternIndex;

        private bool[] _isInPattern;

        private void Awake()
        {
            _pattern = new List<int>();

            _buttons = gameObject.GetComponentsInChildren<ColorButtonCompletable>();
            _isInPattern = new bool[_buttons.Length];
        }

        public void ResetValues()
        {
            _pattern.Clear();

            _buttons = gameObject.GetComponentsInChildren<ColorButtonCompletable>();
            _isInPattern = new bool[_buttons.Length];
        }

        public void CreatePattern()
        {
            _pattern.Clear();
            if (canRepeat)
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
                _isInPattern[num] = true;
            }

            var sb = new StringBuilder();

            foreach (var i in _pattern)
            {
                sb.Append(i + ",");
            }

            sb.Remove(sb.Length - 1, 1);

            print(sb.ToString());
        }

        private void CreateNonRepeatingPattern()
        {
            var count = _buttons.Length;

            for (var i = 0; i < patternLength; i++)
            {
                if (_pattern.Count == _buttons.Length) return;

                var num = Random.Range(0, count);

                if (_pattern.Contains(num)) continue;

                _pattern.Add(num);
                _isInPattern[num] = true;
            }
        }


        private void CheckCompletion()
        {
            for (var buttonIndex = 0; buttonIndex < _buttons.Length; buttonIndex++)
            {
                if ((!_isInPattern[buttonIndex] // Button is not in the pattern
                     // Button is in the pattern but not the next one
                     || (_isInPattern[buttonIndex] && _pattern[_patternIndex] != buttonIndex))
                    && _buttons[buttonIndex].IsDone) // Button is pressed
                {
                    Failed();
                    return;
                }

                if (!_isInPattern[buttonIndex]
                    || !_buttons[buttonIndex].IsDone
                    || _pattern[_patternIndex] != buttonIndex) continue;

                _buttons[buttonIndex].ResetState();
                _patternIndex++;

                if (_patternIndex != _pattern.Count) continue;

                Completed();
                return;
            }

            OnIncompleteCheck.Invoke();
        }

        private void Completed()
        {
            _patternIndex = 0;
            BlinkCorrectButtons();

            IsDone = true;
            OnDone.Invoke();
        }

        private void Failed()
        {
            BlinkIncorrectButtons();
            OnFailedCheck.Invoke();
            ResetState();
        }

        public void BlinkButtons()
        {
            StopCoroutine(BlinkButtonsCoroutine());
            StartCoroutine(BlinkButtonsCoroutine());
        }

        private IEnumerator BlinkButtonsCoroutine()
        {
            yield return new WaitForSeconds(1);

            foreach (var index in _pattern)
            {
                yield return StartCoroutine(_buttons[index].Blink(blinkDuration));
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void BlinkCorrectButtons()
        {
            foreach (var colorButtonCompletable in _buttons)
            {
                StartCoroutine(colorButtonCompletable.BlinkCorrectColor(blinkDuration));
            }
        }

        private void BlinkIncorrectButtons()
        {
            foreach (var colorButtonCompletable in _buttons)
            {
                StartCoroutine(colorButtonCompletable.BlinkIncorrectColor(blinkDuration));
            }
        }

        public override void ResetState()
        {
            base.ResetState();

            foreach (var button in _buttons)
            {
                button.ResetState();
            }

            _patternIndex = 0;
        }

        protected void OnEnable()
        {
            foreach (var completable in _buttons)
            {
                completable.OnDone.AddListener(CheckCompletion);
            }
        }

        protected void OnDisable()
        {
            foreach (var completable in _buttons)
            {
                completable.OnDone.RemoveListener(CheckCompletion);
            }
        }
    }
}