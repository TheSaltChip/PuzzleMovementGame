using System.Collections.Generic;
using Difficulty;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class HighlightButtonIfDifficulty : MonoBehaviour
    {
        [SerializeField] private LevelDifficulty valueOfThis;
        [SerializeField] private Button button;

        private Color _normalColor;
        private Color _selectedColor;

        private void Awake()
        {
            _normalColor = button.colors.normalColor;
            _selectedColor = button.colors.selectedColor;
        }

        public void CheckStatus(LevelDifficulty difficulty)
        {
            var cb = button.colors;

            cb.normalColor = _normalColor;

            if (difficulty == valueOfThis)
            {
                cb.normalColor = _selectedColor;
            }

            button.colors = cb;
        }
    }
}