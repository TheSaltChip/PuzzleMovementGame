using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace FigureMatching
{
    public class FigureGameStateWatcher : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        public UnityEvent levelDone;
        
        public void CorrectMatched()
        {
            if (!rules.SubtractMatched()) return;
            levelDone?.Invoke();
        }
    }
}