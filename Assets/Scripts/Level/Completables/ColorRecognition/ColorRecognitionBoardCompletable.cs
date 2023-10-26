using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Level.Completables.ColorRecognition
{
    public class ColorRecognitionBoardCompletable : Completable
    {
        [SerializeField] private bool canRepeat;
        [SerializeField] private int patternLength;
        [SerializeField] private float blinkDuration;

        private List<ColorButtonCompletable> _buttons;
        private List<int> _pattern;
        private int _patternIndex;

        private int[] _isInPattern;

        private void Awake()
        {
            _pattern = new List<int>();

            _buttons = new List<ColorButtonCompletable>();
            _buttons.AddRange(gameObject.GetComponentsInChildren<ColorButtonCompletable>());
            _isInPattern = new int[_buttons.Count];
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
            var count = _buttons.Count;

            for (var i = 0; i < patternLength; i++)
            {
                var num = Random.Range(0, count);
                _pattern.Add(num);
                _isInPattern[num]++;
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
            var count = _buttons.Count;

            for (var i = 0; i < patternLength; i++)
            {
                if (_pattern.Count == _buttons.Count) return;

                var num = Random.Range(0, count);

                if (_pattern.Contains(num)) continue;

                _pattern.Add(num);
                _isInPattern[num]++;
            }
        }


        private void CheckCompletion()
        {
            for (var i = 0; i < _buttons.Count; i++)
            {
                if (
                    _isInPattern[i] > 0 // Button is in the pattern 
                    && _buttons[i].IsDone // and is pressed 
                    && _pattern[_patternIndex] == i // and the current button is the next in the pattern
                )
                {
                    _buttons[i].ResetState();
                    _patternIndex++;

                    if (_patternIndex == _pattern.Count)
                    {
                        Completed();
                        return;
                    }

                    continue;
                }


                // Button is not in the pattern or is not pressed or is not the current button

                if (_isInPattern[i] == 0 && _buttons[i].IsDone) // Button is not in the pattern and is pressed
                {
                    Failed();
                    return;
                }

                if (_isInPattern[i] > 0 && _pattern[_patternIndex] != i &&
                    _buttons[i].IsDone) // Button is in the pattern and pressed but not the next one 
                {
                    Failed();
                    return;
                }
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