using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Variables;

namespace UI
{
    public class HighlightButtonIfSelected : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private StringVariable group;
        [SerializeField] private string id;
        [SerializeField] private bool startSelected;

        public UnityEvent<string, string> onCheckState;

        private Color _normalColor;
        private Color _selectedColor;

        private void Awake()
        {
            _normalColor = button.colors.normalColor;
            _selectedColor = button.colors.selectedColor;

            if (!startSelected)
                return;

            var cb = button.colors;
            
            cb.normalColor = _selectedColor;

            button.colors = cb;
        }

        public void Check()
        {
            onCheckState?.Invoke(group.value, id);
        }

        public void CheckState(string groupName, string idName)
        {
            if (group.value != groupName)
            {
                return;
            }

            var cb = button.colors;

            cb.normalColor = _normalColor;

            if (id == idName)
            {
                cb.normalColor = _selectedColor;
            }

            button.colors = cb;
        }
    }
}