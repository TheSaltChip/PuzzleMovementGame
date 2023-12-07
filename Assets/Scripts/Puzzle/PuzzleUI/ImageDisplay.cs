using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.PuzzleUI
{
    public class ImageDisplay : MonoBehaviour
    {
        [SerializeField] private SelectedImage image;
        
        public void ChangeImage()
        {
            var img = image.currentSelected;
            gameObject.GetComponent<Image>().sprite =
                Sprite.Create(img, new Rect(0.0f,0.0f,img.width, img.height), new Vector2(0.5f, 0.5f));
        }
    }
}
