using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Puzzle.PuzzleUI
{
    public class intToSO : MonoBehaviour
    {
        [SerializeField] private IntVariable var;
        public UnityEvent ev;

        public void Increment()
        {
            var.value++;
            ev.Invoke();
        }

        public void Decrement()
        {
            var.value--;
        }
    }
}
