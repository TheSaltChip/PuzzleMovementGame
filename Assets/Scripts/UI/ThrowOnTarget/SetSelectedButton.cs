using UnityEngine;
using UnityEngine.Events;

namespace UI.ThrowOnTarget
{
    public class SetSelectedButton : MonoBehaviour
    {
        [SerializeField] private SelectedLevelReference selectedLevelReference;

        public UnityEvent onButtonSet;

        public void SetButton(ReferenceToLevelFile referenceToLevelFile)
        {
            selectedLevelReference.referenceToLevelFile = referenceToLevelFile;
            onButtonSet.Invoke();
        }
    }
}