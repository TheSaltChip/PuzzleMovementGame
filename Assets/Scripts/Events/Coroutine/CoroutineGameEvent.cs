using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events.Coroutine
{
    [CreateAssetMenu(fileName = "ColorGameEvent", menuName = "Events/CoroutineGameEvent")]
    public class CoroutineGameEvent : ScriptableObject
    {
        private List<CoroutineGameEventListener> _listeners = new();

        public void Raise()
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(CoroutineGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(CoroutineGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}