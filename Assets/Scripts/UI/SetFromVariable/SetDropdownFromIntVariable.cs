using TMPro;
using UnityEngine;
using Variables;

namespace UI.SetFromVariable
{
    public class SetDropdownFromIntVariable : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown turnDropdown;
        [SerializeField] private IntVariable variable;

        public void Set()
        {
            turnDropdown.SetValueWithoutNotify(variable.value);
        }
    }
}