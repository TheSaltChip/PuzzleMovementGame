using UnityEngine;

namespace Events.String
{
    public class InvokeStringGameEvent : MonoBehaviour
    {
        [SerializeField] private string str;
        [SerializeField] private StringGameEvent gameEvent;

        public void Invoke()
        {
            gameEvent.Raise(str);
        }
    }
}