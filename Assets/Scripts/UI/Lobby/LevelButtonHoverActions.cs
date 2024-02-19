using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Lobby
{
    public class LevelButtonHoverActions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string stringEntry;

        public UnityEvent<string> onEnter;
        public UnityEvent<string> onExit;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke(stringEntry);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke(stringEntry);
        }

        public void SetStringEntry(string entry)
        {
            stringEntry = entry;
        }
    }
}