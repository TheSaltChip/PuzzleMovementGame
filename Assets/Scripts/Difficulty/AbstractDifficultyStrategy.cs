using UnityEngine;
using UnityEngine.Events;

namespace Difficulty
{
    public abstract class AbstractDifficultyStrategy : ScriptableObject
    {
        public abstract void SetDifficulty(LevelDifficulty difficulty);
    }
}