using System.Collections.Generic;
using UnityEngine;

namespace Events.Vec2IntGameEvent
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Vector2IntGameEvent")]
    public class Vector2IntGameEvent : ScriptableObject
    {
        private List<Vector2IntGameEventListener> _listeners = new();

        public void Raise(Vector2Int value)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(Vector2IntGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(Vector2IntGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}