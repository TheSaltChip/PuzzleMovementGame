using UnityEngine;

namespace Difficulty
{
    public abstract class AbstractDifficultyStrategy : ScriptableObject
    {
        public abstract void SetDifficulty(LevelDifficulty difficulty);
    }
}