using UnityEngine;
using UnityEngine.Events;

namespace Events.Color
{
    public class ColorGameEventListener : MonoBehaviour
    {
        public ColorGameEvent Event;
        public UnityEvent<UnityEngine.Color> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(UnityEngine.Color color)
        {
            response.Invoke(color);
        }
    }
}