using UnityEngine;
using Variables;

namespace Memorization.Figures
{
    [CreateAssetMenu(fileName = "FigureMatchingRules", menuName = "Memorization/Figure/FigureMatchingRules")]
    public class FigureMatchingRules : ScriptableObject
    {
        public MatchingRule matchingRule;
        public int numColor;
        public int numFigure;
    }
}