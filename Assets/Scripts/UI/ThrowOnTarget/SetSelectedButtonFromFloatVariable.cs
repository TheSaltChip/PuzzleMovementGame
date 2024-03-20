using System.Globalization;
using Events.String;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButtonFromFloatVariable : MonoBehaviour
    {
        [SerializeField] private FloatVariable id;

        public StringGameEvent buttonGroupGameEvent;

        public void Raise()
        {
            buttonGroupGameEvent.Raise(id.value.ToString("0.##",CultureInfo.InvariantCulture));
        }
    }
}