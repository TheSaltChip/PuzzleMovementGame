using System.Collections.Generic;
using UnityEngine;

namespace Events.Audio
{
    [CreateAssetMenu(fileName = "AudioGameEvent", menuName = "Events/AudioGameEvent")]
    public class AudioGameEvent : ScriptableObject
    {
        private List<AudioGameEventListener> _listeners = new();

        public void Raise(AudioClip audioClip)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(audioClip);
            }
        }

        public void RegisterListener(AudioGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(AudioGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}