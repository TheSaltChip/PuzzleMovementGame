using UnityEngine;
using UnityEngine.EventSystems;

namespace Lobby
{
    public class ActivateGateHelperScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Canvas helperCanvas;

        private int _i;

        public void OnPointerEnter(PointerEventData eventData)
        {
            helperCanvas.enabled = true;
            ++_i;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (--_i > 0) return;

            _i = 0;

            helperCanvas.enabled = false;
        }
    }
}