using Autohand;
using UnityEngine;
using Variables;

namespace UI.Options
{
    public class RotationSliderCanvasStateChanger : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RotationType rotationType;
        [SerializeField] private IntVariable currentRotationType;

        public void Change()
        {
            canvas.enabled = rotationType == (RotationType)currentRotationType.value;
        }
    }
}