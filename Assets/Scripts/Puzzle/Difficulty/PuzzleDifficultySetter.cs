
using Puzzle.Scriptables;
using UnityEngine;

namespace Puzzle.Difficulty
{
    [CreateAssetMenu(fileName = "PuzzleDifficultySetter", menuName = "Puzzle/PuzzleDifficultySetter")]
    public class PuzzleDifficultySetter : ScriptableObject
    {
        [SerializeField] private PuzzleBoardDimensions puzzleBoardDimensions;
        [SerializeField] private PuzzleBoardDifficultyStrategy puzzleBoardDifficultyStrategy;

        public void SetDifficultyStrategy(PuzzleBoardDifficultyStrategy strategy)
        {
            puzzleBoardDifficultyStrategy = strategy;
            Set();
        }
        
        public void Set()
        {
            puzzleBoardDimensions.Width = puzzleBoardDifficultyStrategy.dimensions.x;
            puzzleBoardDimensions.Height = puzzleBoardDifficultyStrategy.dimensions.y;
        }
    }
}