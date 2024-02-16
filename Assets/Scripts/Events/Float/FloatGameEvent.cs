using System.Collections.Generic;
using UnityEngine;

namespace Events.Float
{
    [CreateAssetMenu(fileName = "FloatGameEvent",menuName = "Events/FloatGameEvent")]
    public class FloatGameEvent : ScriptableObject
    {
        private List<FloatGameEventListener> _listeners = new();

        public void Raise(float value)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(FloatGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(FloatGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}