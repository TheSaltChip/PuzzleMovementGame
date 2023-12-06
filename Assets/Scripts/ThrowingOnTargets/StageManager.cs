using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace ThrowingOnTargets
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private IntVariable targetsLeftInStage;
        [SerializeField] private ThrowLevelSO currentLevel;

        public UnityEvent onStageDone;
        public UnityEvent onLevelDone;

        public void CheckLevelStatus()
        {
            if (targetsLeftInStage.value > 0) return;

            ++currentLevel.currentStage.Value;

            if (currentLevel.currentStage.Value == currentLevel.stages.Length)
            {
                print("done");
                onLevelDone?.Invoke();
                return;
            }

            onStageDone?.Invoke();
        }
    }
}