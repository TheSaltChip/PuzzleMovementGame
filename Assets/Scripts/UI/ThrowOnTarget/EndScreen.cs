using ThrowingOnTargets.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.ThrowOnTarget
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private ThrowLevelSO level;

        public void LevelCompleted()
        {
            text.text = level.levelName;
        }
    }
}
