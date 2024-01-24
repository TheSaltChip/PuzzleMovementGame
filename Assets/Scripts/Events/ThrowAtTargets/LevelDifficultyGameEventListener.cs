using Difficulty;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ThrowAtTargets
{
    public class LevelDifficultyGameEventListener : MonoBehaviour
    {
        public LevelDifficultyGameEvent Event;
        public UnityEvent<LevelDifficulty> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(LevelDifficulty difficulty)
        {
            response.Invoke(difficulty);
        }
    }
}