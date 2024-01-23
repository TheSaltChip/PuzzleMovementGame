using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FigureMatching
{
    public class SetFromFigureMatchingRuleShapes : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private Slider slider;

        public void Set()
        {
            slider.value = rules.MaxNumShapes;
        }
    }
}