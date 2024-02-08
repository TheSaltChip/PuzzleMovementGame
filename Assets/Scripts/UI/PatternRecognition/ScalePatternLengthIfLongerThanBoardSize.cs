using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace UI.PatternRecognition
{
    public class ScalePatternLengthIfLongerThanBoardSize : MonoBehaviour
    {
        [SerializeField] private IntVariable patternLength;
        [SerializeField] private BoolVariable repeatButtons;

        public UnityEvent onCheckSucceeded;

        public void Check(Vector2Int dim)
        {
            if (repeatButtons.value) return;
            if (patternLength.value <= dim.x * dim.y) return;
            
            patternLength.value = dim.x * dim.y;
            onCheckSucceeded?.Invoke();
        }
    }
}