using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI
{
    public class SetSliderValueFromIntVariable : MonoBehaviour
    {
        [SerializeField] private IntVariable variable;
        [SerializeField] private Slider slider;

        private void Awake()
        {
            slider.value = variable.value;
        }
    }
}