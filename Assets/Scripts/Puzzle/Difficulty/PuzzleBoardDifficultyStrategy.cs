using UnityEngine;

namespace Puzzle.Difficulty
{
    [CreateAssetMenu(fileName = "PuzzleBoardDifficultyValue", menuName = "Puzzle/PuzzleBoardDifficultyValue")]
    public class PuzzleBoardDifficultyStrategy : ScriptableObject
    {
        public Vector2Int dimensions;
    }
}