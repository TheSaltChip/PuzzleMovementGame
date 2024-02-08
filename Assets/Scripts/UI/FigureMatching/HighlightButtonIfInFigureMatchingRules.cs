using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class HighlightButtonIfInFigureMatchingRules : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules figureMatchingRules;
        [SerializeField] private int valueOfThis;
        [SerializeField] private Button button;

        private Color _normalColor;
        private Color _selectedColor;

        private void Awake()
        {
            _normalColor = button.colors.normalColor;
            _selectedColor = button.colors.selectedColor;
        }

        public void CheckStatus()
        {
            var cb = button.colors;

            cb.normalColor = _normalColor;

            if (valueOfThis == figureMatchingRules.NumToMatch)
            {
                cb.normalColor = _selectedColor;
            }
            
            button.colors = cb;
        }
    }
}