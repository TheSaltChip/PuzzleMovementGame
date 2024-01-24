using UnityEngine;
using UnityEngine.Events;

namespace Events.Vec2IntGameEvent
{
    public class Vector2IntGameEventListener : MonoBehaviour
    {
        public Vector2IntGameEvent Event;
        public UnityEvent<Vector2Int> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Vector2Int value)
        {
            response?.Invoke(value);
        }
    }
}