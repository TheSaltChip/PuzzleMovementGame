using UnityEngine;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "SelectedImage", menuName = "Puzzle/SelectedImage", order = 0)]
    public class SelectedImage : ScriptableObject
    {
        public Texture2D currentSelected;
    }
}