
using Puzzle.PuzzleUI;
using Puzzle.Scriptables;
using UnityEditor;
using UnityEngine;
using Variables;

namespace Puzzle.Difficulty
{
    [CreateAssetMenu(fileName = "PuzzleDifficultySetter", menuName = "Puzzle/PuzzleDifficultySetter")]
    public class PuzzleDifficultySetter : ScriptableObject
    {
        [SerializeField] private PuzzleBoardDimensions puzzleBoardDimensions;
        [SerializeField] private PuzzleBoardDifficultyStrategy puzzleBoardDifficultyStrategy;
        [SerializeField] private JigsawImages images;
        [SerializeField] private SelectedImage chosenImage;

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
                chosenImage.currentSelected = images.GetRandomTexture2D();
            }
        }
    }
}