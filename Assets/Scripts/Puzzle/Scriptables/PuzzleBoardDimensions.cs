using UnityEngine;
using Variables;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "PuzzleBoardDimensions", menuName = "Puzzle/PuzzleBoardDimensions", order = 0)]
    public class PuzzleBoardDimensions : ScriptableObject
    {
        public IntVariable height;
        public IntVariable width;

        public int Height
        {
            get => height.value;
            set => height.value = value;
        }

        public int Width
        {
            get => width.value;
            set => width.value = value;
        }
    }
}