﻿using UnityEngine;
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

        public void SetDefaultValues()
        {
            gridDimension.value = new Vector2Int(3, 3);
            patternLength.value = 0;
            patternIndex.value = 0;
            maxPatternLength.value = 20;
            bestScore.value = 0;
            canRepeat.value = true;
            isPatternCreated.value = false;
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
    }
}