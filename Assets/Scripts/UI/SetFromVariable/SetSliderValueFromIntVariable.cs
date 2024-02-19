using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI.SetFromVariable
{
    public class SetSliderValueFromIntVariable : MonoBehaviour
    {
        [SerializeField] private IntVariable variable;
        [SerializeField] private Slider slider;
        [SerializeField] private float factor = 1f;

        private void Start()
        {
            slider.value = variable.value * factor;
        }

        public void SetValue()
        {
            slider.value = variable.value * factor;
        }
    }
}