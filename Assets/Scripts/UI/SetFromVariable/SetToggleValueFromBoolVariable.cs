using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace UI.SetFromVariable
{
    public class SetToggleValueFromBoolVariable : MonoBehaviour
    {
        [SerializeField] private BoolVariable variable;
        [SerializeField] private Toggle toggle;

        private void Start()
        {
            toggle.isOn = variable.value;
        }

        public void SetValueWithoutNotify()
        {
            toggle.SetIsOnWithoutNotify(variable.value);
        }
    }
}