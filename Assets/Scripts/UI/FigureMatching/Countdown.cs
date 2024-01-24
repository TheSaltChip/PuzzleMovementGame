﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Variables;

namespace UI.FigureMatching
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField, Min(0)] private int startNumber;
        [SerializeField] private BoolVariable canStartCountdown;

        private WaitForSeconds _oneSecond;
        private int _num;

        public UnityEvent tick;
        public UnityEvent countdownCompleted;

        private void Awake()
        {
            _oneSecond = new WaitForSeconds(1f);
        }

        public void StartCountdown()
        {
            _num = startNumber;
            text.text = _num.ToString();
            StartCoroutine(StartCountdownCoroutine());
        }

        private IEnumerator StartCountdownCoroutine()
        {
            yield return new WaitUntil(() => canStartCountdown.value);
            
            while (_num > 0)
            {
                yield return _oneSecond;
                --_num;
                tick?.Invoke();
                text.text = _num.ToString();
            }

            countdownCompleted?.Invoke();
        }
    }
}