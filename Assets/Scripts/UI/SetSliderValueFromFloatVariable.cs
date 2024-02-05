using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI
{
    public class SetSliderValueFromFloatVariable : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private Slider slider;

        public void Set()
        {
            slider.value = variable.value;
        }
    }
}