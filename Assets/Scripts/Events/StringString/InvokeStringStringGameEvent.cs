using UnityEngine;

namespace Events.StringString
{
    public class InvokeStringStringGameEvent : MonoBehaviour
    {
        [SerializeField] private string string1;
        [SerializeField] private string string2;
        [SerializeField] private StringStringGameEvent gameEvent;

        public void Invoke()
        {
            gameEvent.Raise(string1, string2);
        }
    }
}

namespace Events.String
{
}