using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.PuzzleUI
{
    public class ChosenImage : MonoBehaviour
    {
        [SerializeField] private Texture2D image;
        [SerializeField] private SelectedImage store;
        public UnityEvent completed;

        public void SetImage(Texture2D texture)
        {
            image = texture;
        }
        
        public void ChangeImage()
        {
            store.currentSelected = image;
            completed.Invoke();
        }
    }
}
