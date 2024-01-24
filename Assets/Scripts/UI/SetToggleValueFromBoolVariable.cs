using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI
{
    public class SetToggleValueFromBoolVariable : MonoBehaviour
    {
        [SerializeField] private BoolVariable variable;
        [SerializeField] private Toggle slider;

        private void Awake()
        {
            slider.isOn = variable.value;
        }

        public void SetValue()
        {
            slider.isOn = variable.value;
        }
    }
}