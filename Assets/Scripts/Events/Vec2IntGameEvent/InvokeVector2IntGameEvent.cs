using UnityEngine;
using UnityEngine.Events;

namespace Events.Vec2IntGameEvent
{
    public class InvokeVector2IntGameEvent : MonoBehaviour
    {
        [SerializeField] private Vector2Int val;

        public UnityEvent<Vector2Int> onEvent;

        public void Invoke()
        {
            onEvent.Invoke(val);
        }
    }
}