using System.Collections.Generic;
using UnityEngine;

namespace Events.LevelDifficulty
{
    [CreateAssetMenu(fileName = "LevelDifficultyGameEvent", menuName = "Events/LevelDifficultyGameEvent")]
    public class LevelDifficultyGameEvent : ScriptableObject
    {
        private readonly List<LevelDifficultyGameEventListener> _listeners = new();

        public void Raise(Difficulty.LevelDifficulty difficulty)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(difficulty);
            }
        }

        public void RegisterListener(LevelDifficultyGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(LevelDifficultyGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}