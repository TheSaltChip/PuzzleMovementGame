using UnityEngine;

namespace Puzzle.PuzzleUI
{
    public class FlipUIState : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        public void ChangeState()
        { 
            obj.SetActive(!obj.activeSelf);
        }
    }
}
