using UnityEngine;
using UnityEngine.Events;

namespace Events.Color
{
    public class NamedGameEventListener : MonoBehaviour
    {
        public NamedGameEvent Event;
        public UnityEvent<Object> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Object obj)
        {
            response.Invoke(obj);
        }
    }
}