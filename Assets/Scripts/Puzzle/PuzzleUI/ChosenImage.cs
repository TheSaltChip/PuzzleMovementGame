using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.PuzzleUI
{
    public class ChosenImage : MonoBehaviour
    {
        [SerializeField] private Sprite image;
        [SerializeField] private SelectedSprite store;
        public UnityEvent completed;

        public void SetImage(Sprite sprite)
        {
            image = sprite;
        }
        
        public void ChangeImage()
        {
            store.sprite = image;
            completed.Invoke();
        }
    }
}
