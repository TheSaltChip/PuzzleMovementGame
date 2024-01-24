using UnityEngine;
using UnityEngine.Events;

namespace Events.Int
{
    public class IntGameEventListener : MonoBehaviour
    {
        public IntGameEvent Event;
        public UnityEvent<int> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int value)
        {
            response?.Invoke(value);
        }
    }
}