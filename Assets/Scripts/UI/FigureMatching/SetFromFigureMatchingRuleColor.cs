using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class SetFromFigureMatchingRuleColor : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private Slider slider;

        public void Set()
        {
            slider.value = rules.MaxNumColor;
        }
    }
}