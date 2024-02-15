using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetSliderMaxValue : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void Set(int max)
        {
            slider.maxValue = max;
        }
    }
}