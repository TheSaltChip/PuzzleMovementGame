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
using Variables;

namespace PatternRecognition.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PatternRecognitionRules", menuName = "PatternRecognition/PatternRecognitionRules")]
    public class PatternRecognitionRules : ScriptableObject
    {
        public Vector2IntVariable gridDimension;
        public IntVariable patternLength;
        public IntVariable patternIndex;
        public IntVariable maxPatternLength;
        public IntVariable bestScore;
        public BoolVariable canRepeat;
        public BoolVariable isPatternCreated;
        public BoolVariable continuousMode;

        public void SetDefaultValues()
        {
            gridDimension.value = new Vector2Int(3, 3);
            patternLength.value = 6;
            patternIndex.value = 0;
            maxPatternLength.value = 20;
            bestScore.value = 0;
            canRepeat.value = true;
            isPatternCreated.value = false;
            continuousMode.value = true;
        }

        public Vector2Int GridDimension
        {
            get => gridDimension.value;
            set => gridDimension.value = value;
        }

        public int PatternLength
        {
            get => patternLength.value;
            set => patternLength.value = value;
        }

        public int PatternIndex
        {
            get => patternIndex.value;
            set => patternIndex.value = value;
        }

        public int MaxPatternLength
        {
            get => maxPatternLength.value;
            set => maxPatternLength.value = value;
        }

        public int BestScore
        {
            get => bestScore.value;
            set => bestScore.value = value;
        }

        public bool CanRepeat
        {
            get => canRepeat.value;
            set => canRepeat.value = value;
        }

        public bool IsPatternCreated
        {
            get => isPatternCreated.value;
            set => isPatternCreated.value = value;
        }
        
        public bool ContinuousMode
        {
            get => continuousMode.value;
            set => continuousMode.value = value;
        }
    }
}