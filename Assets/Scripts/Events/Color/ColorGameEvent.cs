using System.Collections.Generic;
using UnityEngine;

namespace Events.Color
{
    [CreateAssetMenu(fileName = "ColorGameEvent", menuName = "Events/ColorGameEvent")]
    public class ColorGameEvent : ScriptableObject
    {
        private List<ColorGameEventListener> _listeners = new();

        public void Raise(UnityEngine.Color color)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(color);
            }
        }

        public void RegisterListener(ColorGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(ColorGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}