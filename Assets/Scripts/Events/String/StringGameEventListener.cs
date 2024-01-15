using UnityEngine;
using UnityEngine.Events;

namespace Events.String
{
    public class StringGameEventListener : MonoBehaviour
    {
        public StringGameEvent Event;
        public UnityEvent<string> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(string str)
        {
            response.Invoke(str);
        }
    }
}