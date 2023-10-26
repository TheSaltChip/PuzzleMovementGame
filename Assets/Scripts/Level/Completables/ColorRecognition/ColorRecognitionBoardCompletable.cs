using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        private void Awake()
        {
            _pattern = new List<int>();
            _buttons = new List<ColorButtonCompletable>();
            _buttons.AddRange(gameObject.GetComponentsInChildren<ColorButtonCompletable>());
            OnFailedCheck.AddListener(BlinkButtons);
            
            
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
                _pattern.Add(Random.Range(0, count));
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
                if(_pattern.Count == _buttons.Count) return;
                
                var num = Random.Range(0, count);
                if(!_pattern.Contains(num))
                    _pattern.Add(num);
            }
        }


        private void CheckCompletion()
        {
            print("Hello");
            for (var i = 0; i < _pattern.Count; i++)
            {
                if (_buttons[_pattern[i]].IsDone) continue;
                
                CheckRestOfList(i);
                return;
            }
            
            OnDone.Invoke();
        }
        
        private void CheckRestOfList(int start)
        {
            for (var j = start; j < _pattern.Count; j++)
            {
                if (!_buttons[_pattern[j]].IsDone) continue;

                OnFailedCheck.Invoke();
                ResetState();
                return;
            }

            OnIncompleteCheck.Invoke();
        }

        public void BlinkButtons()
        {
            StartCoroutine(BlinkButtonsCoroutine());
        }

        private IEnumerator BlinkButtonsCoroutine()
        {
            foreach (var index in _pattern)
            {
                yield return StartCoroutine(_buttons[index].Blink(blinkDuration));
            }
        }

        public override void ResetState()
        {
            base.ResetState();

            foreach (var button in _buttons)
            {
                button.ResetState();
            }
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