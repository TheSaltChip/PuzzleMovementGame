using System.Globalization;
using Events.StringString;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButtonFromFloatVariable : MonoBehaviour
    {
        [SerializeField] private StringVariable groupName;
        [SerializeField] private FloatVariable id;

        public StringStringGameEvent buttonGroupGameEvent;

        public void Raise()
        {
            buttonGroupGameEvent.Raise(groupName.value, id.value.ToString(CultureInfo.InvariantCulture));
        }
    }
}