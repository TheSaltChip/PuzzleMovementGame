using UnityEngine;
using UnityEngine.Events;

namespace Events.FigureInfo
{
    public class FigureInfoGameEventListener : MonoBehaviour
    {
        public FigureInfoGameEvent Event;
        public UnityEvent<FigureMatching.FigureInfo> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(FigureMatching.FigureInfo info)
        {
            response?.Invoke(info);
        }
    }
}