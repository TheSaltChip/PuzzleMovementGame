using PatternRecognition.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UI.PatternRecognition
{
    public class ScalePatternLengthIfLongerThanBoardSize : MonoBehaviour
    {
        [SerializeField] private PatternRecognitionRules patternRecognitionRules;

        public UnityEvent<int> patternMaxLength;

        public void Check()
        {
            var dim = patternRecognitionRules.GridDimension;

            if (patternRecognitionRules.CanRepeat)
            {
                patternMaxLength?.Invoke(patternRecognitionRules.MaxPatternLength);
                return;
            }

            if (patternRecognitionRules.patternLength.value > dim.x * dim.y)
                patternRecognitionRules.PatternLength = dim.x * dim.y;


            patternMaxLength?.Invoke(dim.x * dim.y);
        }
    }
}