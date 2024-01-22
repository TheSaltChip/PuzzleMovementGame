using UnityEngine;
using UnityEngine.Events;

namespace Events.StringString
{
    public class StringStringGameEventListener : MonoBehaviour
    {
        public StringStringGameEvent Event;
        public UnityEvent<string, string> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(string str1, string str2)
        {
            response.Invoke(str1, str2);
        }
    }
}