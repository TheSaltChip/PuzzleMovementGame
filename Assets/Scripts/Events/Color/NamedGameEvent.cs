using System.Collections.Generic;
using UnityEngine;

namespace Events.Color
{
    [CreateAssetMenu(fileName = "NamedGameEvent", menuName = "Events/NamedGameEvent")]
    public class NamedGameEvent : ScriptableObject
    {
        private List<NamedGameEventListener> _listeners = new();

        public void Raise(Object obj)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(obj);
            }
        }

        public void RegisterListener(NamedGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(NamedGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}