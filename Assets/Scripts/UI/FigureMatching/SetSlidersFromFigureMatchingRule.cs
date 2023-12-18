using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class SetSlidersFromFigureMatchingRule : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private Slider numShapes;
        [SerializeField] private Slider numColors;
        [SerializeField] private Slider numTotal;

        public void Set()
        {
            numShapes.value = rules.MaxNumShapes;
            numColors.value = rules.MaxNumColor;
            numTotal.value = rules.TotalTotalNumberOfFigures;
        }
    }
}