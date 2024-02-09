using PatternRecognition.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UI.PatternRecognition
{
    public class ScalePatternLengthIfLongerThanBoardSize : MonoBehaviour
    {
        [SerializeField] private PatternRecognitionRules patternRecognitionRules;

        public UnityEvent onCheckSucceeded;

        public void Check()
        {
            if (patternRecognitionRules.CanRepeat) return;

            var dim = patternRecognitionRules.GridDimension;
            
            if (patternRecognitionRules.patternLength.value <= dim.x * dim.y) return;
            
            patternRecognitionRules.PatternLength = dim.x * dim.y;
            onCheckSucceeded?.Invoke();
        }
    }
}