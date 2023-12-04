using Difficulty;
using Events.ThrowAtTargets;
using UnityEngine;

namespace UI.Difficulty
{
    public class RaiseEventWithDifficulty : MonoBehaviour
    {
        [SerializeField] private LevelDifficultyGameEvent gameEvent;
        [SerializeField] private LevelDifficulty difficulty;
        
        public void Raise()
        {
            gameEvent.Raise(difficulty);
        }
    }
}