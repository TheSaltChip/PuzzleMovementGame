using UnityEngine;

namespace Events.LevelDifficulty
{
    public class InvokeLevelDifficultyGameEvent : MonoBehaviour
    {
        [SerializeField] private LevelDifficultyGameEvent gameEvent;
        [SerializeField] private Difficulty.LevelDifficulty difficulty;
        
        public void Raise()
        {
            gameEvent.Raise(difficulty);
        }
    }
}