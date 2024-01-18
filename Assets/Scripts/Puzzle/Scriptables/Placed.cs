using UnityEngine;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "Placed", menuName = "Puzzle/Placed", order = 0)]
    public class Placed : ScriptableObject
    {
        public int amount;
        public string number;
        public GameObject piece;
    }
}