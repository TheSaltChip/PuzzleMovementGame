using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace ThrowingOnTargets
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private IntVariable targetsLeftInStage;

        public UnityEvent onLevelDone;

        public void CheckLevelStatus()
        {
            if (targetsLeftInStage.value > 0) return;
            
            onLevelDone?.Invoke();
        }
    }
}