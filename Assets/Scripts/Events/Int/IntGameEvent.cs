using System.Collections.Generic;
using UnityEngine;

namespace Events.Int
{
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "Events/IntGameEvent")]
    public class IntGameEvent : ScriptableObject
    {
        private List<IntGameEventListener> _listeners = new();

        public void Raise(int value)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(IntGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(IntGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}