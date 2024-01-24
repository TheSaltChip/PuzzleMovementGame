using System.Collections.Generic;
using Memorization.Figure;
using UnityEngine;

namespace Events.FigureMatching
{
    [CreateAssetMenu(fileName = "FigureInfoGameEvent", menuName = "Events/FigureInfoGameEvent")]
    public class FigureInfoGameEvent : ScriptableObject
    {
        private List<FigureInfoGameEventListener> _listeners = new();

        public void Raise(FigureInfo info)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(info);
            }
        }

        public void RegisterListener(FigureInfoGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(FigureInfoGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}