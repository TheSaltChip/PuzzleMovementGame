using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Memorization.Figure
{
    public class FigureGameStateWatcher : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        public UnityEvent levelDone;

        public void CorrectMatched()
        {
            if (rules.SubtractMatched()) levelDone?.Invoke();
        }
    }
}