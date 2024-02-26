using UnityEngine;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "Placed", menuName = "Puzzle/Placed")]
    public class Placed : ScriptableObject
    {
        public int amount;
        public int number;
        public GameObject piece;
    }
}