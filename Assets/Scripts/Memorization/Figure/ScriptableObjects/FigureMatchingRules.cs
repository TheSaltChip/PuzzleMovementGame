using NaughtyAttributes;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureMatchingRules", menuName = "Memorization/Figure/FigureMatchingRules")]
    public class FigureMatchingRules : ScriptableObject
    {
        public MatchingRule matchingRule;

        [HideIf("matchingRule", MatchingRule.Figure)]
        public int numColor;

        [HideIf("matchingRule", MatchingRule.Color)]
        public int numFigure;

        public int numToMatch;
    }
}