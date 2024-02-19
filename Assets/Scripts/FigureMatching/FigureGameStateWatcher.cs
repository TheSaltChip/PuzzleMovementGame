using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FigureMatching
{
    public class FigureGameStateWatcher : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        public UnityEvent onLevelDone;
        public UnityEvent onLevelIncomplete;
        
        public void CorrectMatched()
        {
            if (!rules.SubtractMatched())
            {
                onLevelIncomplete?.Invoke();
                return;
            }
            
            onLevelDone?.Invoke();
        }
    }
}