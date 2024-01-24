using UnityEngine;

namespace Puzzle.PuzzleUI
{
    public class HideUI : MonoBehaviour
    {
        [SerializeField] private GameObject child;
        public void Reveal()
        {
            child.SetActive(true);
        }

        public void Hide()
        {
            child.SetActive(false);
        }
    }
}