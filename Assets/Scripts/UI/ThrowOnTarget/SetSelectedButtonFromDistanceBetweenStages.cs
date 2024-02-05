using Events.String;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButtonFromDistanceBetweenStages : MonoBehaviour
    {
        [SerializeField] private FloatVariable distanceBetweenStages;
        [SerializeField] private StringGameEvent buttonSelectedEvent;

        public void Set()
        {
            var id = distanceBetweenStages.value switch
            {
                1f => "1",
                1.5f => "2",
                2f => "3",
                3f => "4",
                _ => "0"
            };

            buttonSelectedEvent.Raise(id);
        }
    }
}