using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Memorization.Figure
{
    public class FigureGameStateWatcher : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;
        [SerializeField] private IntVariable figuresLeft;

        public UnityEvent levelDone;
        
        public void Setup()
        {
            figuresLeft.value = rules.TotalTotalNumberOfFigures;
        }

        public void CorrectMatched()
        {
            figuresLeft.value -= rules.NumToMatch;

            if (figuresLeft.value <= 0) levelDone?.Invoke();
        }
    }
}