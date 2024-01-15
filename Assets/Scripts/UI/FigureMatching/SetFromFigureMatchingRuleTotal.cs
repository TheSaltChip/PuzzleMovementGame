using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class SetFromFigureMatchingRuleTotal : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private Slider slider;

        public void Set()
        {
            slider.value = rules.TotalTotalNumberOfFigures;
        }
    }
}