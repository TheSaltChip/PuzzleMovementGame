using FigureMatching;
using UnityEngine;
using UnityEngine.Events;

namespace Events.FigureMatching
{
    public class FigureInfoGameEventListener : MonoBehaviour
    {
        public FigureInfoGameEvent Event;
        public UnityEvent<FigureInfo> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(FigureInfo info)
        {
            response?.Invoke(info);
        }
    }
}