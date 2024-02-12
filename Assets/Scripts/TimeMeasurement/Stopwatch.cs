using UnityEngine;
using Variables;

namespace TimeMeasurement
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private FloatVariable timeSpent;
        private bool _startTime;

        public void ResetTime()
        {
            timeSpent.value = 0;
            _startTime = false;
        }
        
        public void StartTime()
        {
            _startTime = true;
        }

        public void StopTime()
        {
            _startTime = false;
        }

        public void RestartTime()
        {
            timeSpent.value = 0;
            _startTime = true;
        }

        private void Update()
        {
            if (!_startTime) return;

            timeSpent.value += Time.deltaTime;
        }
    }
}