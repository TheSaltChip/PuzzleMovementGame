using Events.String;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButtonFromIntVariable : MonoBehaviour
    {
        [SerializeField] private IntVariable id;

        public StringGameEvent buttonGroupGameEvent;

        public void Raise()
        {
            buttonGroupGameEvent.Raise(id.value.ToString());
        }
    }
}