using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI.SetFromVariable
{
    public class SetToggleValueFromBoolVariable : MonoBehaviour
    {
        [SerializeField] private BoolVariable variable;
        [SerializeField] private Toggle slider;

        private void Start()
        {
            slider.isOn = variable.value;
        }

        public void SetValue()
        {
            slider.isOn = variable.value;
        }
    }
}