using UnityEngine;
using UnityEngine.Events;

namespace Events.LevelDifficulty
{
    public class LevelDifficultyGameEventListener : MonoBehaviour
    {
        public LevelDifficultyGameEvent Event;
        public UnityEvent<Difficulty.LevelDifficulty> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(Difficulty.LevelDifficulty difficulty)
        {
            response.Invoke(difficulty);
        }
    }
}