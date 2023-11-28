using Events;
using UnityEngine;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButton : MonoBehaviour
    {
        [SerializeField] private SelectedLevelReference selectedLevelReference;
        [SerializeField] private GameEvent loadLevelInfo;

        public void SetButton(ReferenceToLevelFile referenceToLevelFile)
        {
            selectedLevelReference.ReferenceToLevelFile = referenceToLevelFile;
            loadLevelInfo.Raise();
        }
    }
}