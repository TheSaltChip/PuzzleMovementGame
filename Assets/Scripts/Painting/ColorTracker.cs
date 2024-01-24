using UnityEngine;
using UnityEngine.Events;

namespace Painting
{
    public class ColorTracker : MonoBehaviour
    {
        [SerializeField] private Color color;
        public UnityEvent<Color> press;

        private void OnCollisionEnter(Collision other)
        {
            press.Invoke(color);
            gameObject.SetActive(false);
        }

        public void Activate(Color c)
        {
            gameObject.SetActive(true);
        }
    }
}
