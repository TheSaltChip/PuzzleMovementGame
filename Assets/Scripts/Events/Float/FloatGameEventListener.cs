using UnityEngine;
using UnityEngine.Events;

namespace Events.Float
{
    public class FloatGameEventListener : MonoBehaviour
    {
        public FloatGameEvent Event;
        public UnityEvent<float> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(float value)
        {
            response?.Invoke(value);
        }
    }
}