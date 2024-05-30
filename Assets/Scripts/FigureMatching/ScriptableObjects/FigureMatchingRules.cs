#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

using UnityEngine;

namespace FigureMatching.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureMatchingRules", menuName = "FigureMatching/FigureMatchingRules")]
    public class FigureMatchingRules : ScriptableObject
    {
        [field: SerializeField, Range(1, 10)] public int MaxNumColor { get; set; }

        [field: SerializeField, Range(1, 16)] public int MaxNumShapes { get; set; }

        [field: SerializeField, Range(2, 5)] public int NumToMatch { get; set; }

        [field: SerializeField] public int NumFiguresLeft { get; set; }

        [SerializeField, Range(1, 96),
         Tooltip(
             "This number will be changed to fit the criteria: (this num) % numberOfFiguresToMatch == 0 && (this num) >= numberOfFiguresToMatch")]
        private int totalNumberOfFigures;

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

                NumFiguresLeft = totalNumberOfFigures;
                return totalNumberOfFigures;
            }

            set
            {
                totalNumberOfFigures = value;

                if (totalNumberOfFigures < NumToMatch)
                {
                    totalNumberOfFigures = NumToMatch;
                }

                if (totalNumberOfFigures % NumToMatch != 0)
                {
                    totalNumberOfFigures -= totalNumberOfFigures % NumToMatch;
                }

                NumFiguresLeft = totalNumberOfFigures;
            }
        }

        /// <summary>
        /// Subtracts NumToMatch from NumFiguresLeft
        /// </summary>
        /// <returns>True if number of figures left are &lt;= 0, false if not</returns>
        public bool SubtractMatched()
        {
            NumFiguresLeft -= NumToMatch;

            return NumFiguresLeft <= 0;
        }
    }
}