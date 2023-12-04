using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace ThrowingOnTargets
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private IntVariable targetsLeftInStage;
        [SerializeField] private IntReference currentStage;
        public UnityEvent stageDone;

        private void Awake()
        {
            currentStage.Variable.value = 0;
        }

        public void TargetHit()
        {
            ++currentStage.Value;
            if (currentStage.Value == 4) currentStage.Value = 0;
            
            stageDone?.Invoke();
        }
    }
}