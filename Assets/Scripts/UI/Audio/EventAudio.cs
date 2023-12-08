using UnityEngine;

namespace UI.Audio
{
    [CreateAssetMenu(fileName = "EventAudio", menuName = "Audio/EventAudio")]
    public class EventAudio : ScriptableObject
    {
        public AudioClip onPointerClick;
        public AudioClip onPointerHoverEnter;
        public AudioClip onPointerHoverExit;
    }
}