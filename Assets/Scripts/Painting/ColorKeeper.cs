using UnityEngine;

namespace Painting
{
    public class ColorKeeper : MonoBehaviour
    {
        [SerializeField] private Color _color;

        public Color GetColor()
        {
            return _color;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
    }
}