
using Puzzle.PuzzleUI;
using Puzzle.Scriptables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Variables;

namespace Puzzle.Difficulty
{
    [CreateAssetMenu(fileName = "PuzzleDifficultySetter", menuName = "Puzzle/PuzzleDifficultySetter")]
    public class PuzzleDifficultySetter : ScriptableObject
    {
        [SerializeField] private PuzzleBoardDimensions puzzleBoardDimensions;
        [SerializeField] private PuzzleBoardDifficultyStrategy puzzleBoardDifficultyStrategy;
        [SerializeField] private JigsawImages images;
        [SerializeField] private SelectedSprite chosenSprite;

        public void SetDifficultyStrategy(PuzzleBoardDifficultyStrategy strategy)
        {
            puzzleBoardDifficultyStrategy = strategy;
            Set();
        }
        
        public void Set()
        {
            puzzleBoardDimensions.Width = puzzleBoardDifficultyStrategy.dimensions.x;
            puzzleBoardDimensions.Height = puzzleBoardDifficultyStrategy.dimensions.y;

            if (puzzleBoardDifficultyStrategy.spawnRandomImage)
            {
                chosenSprite.sprite = images.GetRandomImage();
            }
        }
    }
}