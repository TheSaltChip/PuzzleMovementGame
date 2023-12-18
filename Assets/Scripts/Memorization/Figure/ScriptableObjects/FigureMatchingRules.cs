using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureMatchingRules", menuName = "Memorization/Figure/FigureMatchingRules")]
    public class FigureMatchingRules : ScriptableObject
    {
        [field: SerializeField, Range(1, 10)]
        public int MaxNumColor { get; set; }

        [field: SerializeField, Range(1, 16)]
        public int MaxNumShapes { get; set; }

        [field: SerializeField, Range(2, 5)] public int NumToMatch { get; set; }

        [SerializeField, Range(1, 96),
         Tooltip("This number will be changed to fit the criteria: (this num) % numberOfFiguresToMatch == 0 && (this num) >= numberOfFiguresToMatch")]
        private int totalNumberOfFigures;
/*
 * Give button with preset values for total amount, with a toggle that enables slider and hides options
 * Amount to match is given as 4 buttons (2,3,4,5)
 * Give option to play again with the same settings or go back to change some
 * Start screen can have a quick play button (pre defined values) and a custom play button (user decides) 
 */
        public int TotalTotalNumberOfFigures
        {
            get
            {
                if (totalNumberOfFigures < NumToMatch)
                {
                    totalNumberOfFigures = NumToMatch;
                    return totalNumberOfFigures;
                }
                
                if (totalNumberOfFigures % NumToMatch != 0)
                {
                    totalNumberOfFigures -= totalNumberOfFigures % NumToMatch;
                }

                return totalNumberOfFigures;
            }

            set => totalNumberOfFigures = value;
        }
    }
}