using UnityEngine;
using UnityEngine.Events;

namespace Difficulty
{
    public class DifficultySetter : MonoBehaviour
    {
        [SerializeField] private AbstractDifficultyStrategy levelDifficultyStrategy;
        
        public UnityEvent onDifficultySet;

        public void SetDifficulty(LevelDifficulty difficulty)
        {
            levelDifficultyStrategy.SetDifficulty(difficulty);

            onDifficultySet?.Invoke();
        }

        public void SetEasyDifficulty()
        {
            SetDifficulty(LevelDifficulty.Easy);
        }

        public void SetMediumDifficulty()
        {
            SetDifficulty(LevelDifficulty.Medium);
        }

        public void SetHardDifficulty()
        {
            SetDifficulty(LevelDifficulty.Hard);
        }
    }
}