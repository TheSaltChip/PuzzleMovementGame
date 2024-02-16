using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;

namespace UI.FigureMatching
{
    public class SetStringEventEntryOnPointerEnter : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private LocalizeStringEvent stringEvent;
        [SerializeField] private string tableEntry;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            stringEvent.SetEntry(tableEntry);
        }
    }
}