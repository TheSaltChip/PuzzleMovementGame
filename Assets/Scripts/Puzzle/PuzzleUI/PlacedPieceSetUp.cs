using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.PuzzleUI
{
    public class PlacedPieceSetUp : MonoBehaviour
    {
        [SerializeField] private Placed var;
        public UnityEvent placed;
        public UnityEvent removed;

        public void Increment()
        {
            var.amount++;
            placed.Invoke();
        }

        public void Decrement()
        {
            var.amount--;
            removed.Invoke();
        }
    }
}