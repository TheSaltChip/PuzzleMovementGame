using System.Collections.Generic;
using UnityEngine;

namespace Events.StringString
{
    [CreateAssetMenu(fileName = "StringStringGameEvent", menuName = "Events/StringStringGameEvent")]
    public class StringStringGameEvent : ScriptableObject
    {
        private List<StringStringGameEventListener> _listeners = new();

        public void Raise(string str1, string str2)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(str1,str2);
            }
        }

        public void RegisterListener(StringStringGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(StringStringGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}