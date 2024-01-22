using System;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Memorization.Figure
{
    public class FigureGameStateWatcher : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private FloatVariable timeSpent;

        public UnityEvent levelDone;

        private bool _stopTime;

        public void CorrectMatched()
        {
            if (!rules.SubtractMatched()) return;
            
            _stopTime = true;
            levelDone?.Invoke();
        }

        public void StartTime()
        {
            timeSpent.value = 0;
            _stopTime = false;
        }

        public void StopTime()
        {
            _stopTime = true;
        }

        private void Update()
        {
            if (_stopTime) return;

            timeSpent.value += Time.deltaTime;
        }
    }
}