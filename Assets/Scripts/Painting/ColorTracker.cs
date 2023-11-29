using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

namespace Painting
{
    public class ColorTracker : MonoBehaviour
    {
        [SerializeField] private Color color;
        public UnityEvent<Color> press;
        public UnityEvent<Color> changeColor;

        private void OnCollisionEnter(Collision other)
        {
            press.Invoke(color);
            changeColor.Invoke(color);
            gameObject.SetActive(false);
        }

        public void Activate(Color c)
        {
            gameObject.SetActive(true);
        }
    }
}
