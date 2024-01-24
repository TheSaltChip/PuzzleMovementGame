using UnityEngine;
using UnityEngine.Events;

namespace Events.Audio
{
    public class AudioGameEventListener : MonoBehaviour
    {
        public AudioGameEvent Event;
        public UnityEvent<AudioClip> response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(AudioClip audioClip)
        {
            response?.Invoke(audioClip);
        }
    }
}