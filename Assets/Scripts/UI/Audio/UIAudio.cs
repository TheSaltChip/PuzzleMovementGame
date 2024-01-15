using Events.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Audio
{
    public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField] private EventAudio eventAudio;
        [SerializeField] private AudioGameEvent audioGameEvent;

        public EventAudio EventAudio
        {
            set => eventAudio = value;
        }

        public AudioGameEvent AudioGameEvent
        {
            set => audioGameEvent = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            audioGameEvent.Raise(eventAudio.onPointerClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            audioGameEvent.Raise(eventAudio.onPointerHoverEnter);
        }
    }
}