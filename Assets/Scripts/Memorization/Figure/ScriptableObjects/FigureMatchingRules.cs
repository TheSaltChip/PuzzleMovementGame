using NaughtyAttributes;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureMatchingRules", menuName = "Memorization/Figure/FigureMatchingRules")]
    public class FigureMatchingRules : ScriptableObject
    {
        public MatchingRule matchingRule;

        [HideIf("matchingRule", MatchingRule.Figure), Range(1, 10)]
        public int maxNumColor;

        [HideIf("matchingRule", MatchingRule.Color), Range(1, 16)]
        public int maxNumFigure;

        [Range(2, 5)] public int numToMatch;

        [SerializeField, Range(1, 96),
         Tooltip("This number will be changed to fit the criteria: (this num) % numberOfFiguresToMatch == 0")]
        private int totalNumberOfFigures;

        public int TotalTotalNumberOfFigures
        {
            get
            {
                if (totalNumberOfFigures % numToMatch != 0)
                {
                    totalNumberOfFigures -= totalNumberOfFigures % numToMatch;
                }

                return totalNumberOfFigures;
            }

            set => totalNumberOfFigures = value;
        }
    }
}