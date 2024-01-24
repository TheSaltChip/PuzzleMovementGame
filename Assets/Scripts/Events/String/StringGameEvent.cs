using System.Collections.Generic;
using UnityEngine;

namespace Events.String
{
    [CreateAssetMenu(fileName = "StringGameEvent", menuName = "Events/StringGameEvent")]
    public class StringGameEvent : ScriptableObject
    {
        private List<StringGameEventListener> _listeners = new();

        public void Raise(string str)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(str);
            }
        }

        public void RegisterListener(StringGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(StringGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}