using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI.SetFromVariable
{
    public class SetSliderValueFromFloatVariable : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private Slider slider;
        [SerializeField] private float factor = 1f;

        private void Start()
        {
            slider.value = variable.value * factor;
        }
        
        public void Set()
        {
            slider.value = variable.value * factor;
        }
    }
}