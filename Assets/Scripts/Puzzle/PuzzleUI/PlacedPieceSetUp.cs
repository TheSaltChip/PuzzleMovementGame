using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.PuzzleUI
{
    public class PlacedPieceSetUp : MonoBehaviour
    {
        [SerializeField] private Placed var;
        public UnityEvent placed;

        public void Increment()
        {
            var.amount++;
            var.number = gameObject.name;
            placed.Invoke();
        }

        public void Decrement()
        {
            var.amount--;
        }
    }
}