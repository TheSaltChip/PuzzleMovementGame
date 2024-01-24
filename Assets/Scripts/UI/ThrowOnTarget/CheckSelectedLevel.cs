using UnityEngine;
using UnityEngine.Events;

namespace UI.ThrowOnTarget
{
    public class CheckSelectedLevel : MonoBehaviour
    {
        [SerializeField] private SelectedLevelReference selectedLevelReference;
        
        public UnityEvent onCorrectSelection;

        public void CheckIfLevelIsSelected()
        {
            if (selectedLevelReference.referenceToLevelFile == null) return;
            
            onCorrectSelection.Invoke();
        }
    }
}