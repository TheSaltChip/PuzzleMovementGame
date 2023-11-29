using UnityEngine;

namespace UI.ThrowOnTarget
{
    [CreateAssetMenu(fileName = "SelectedLevelReference", menuName = "SelectedLevelReference", order = 0)]
    public class SelectedLevelReference : ScriptableObject
    {
        public ReferenceToLevelFile referenceToLevelFile;

        public void ClearRef()
        {
            referenceToLevelFile = null;
        }
    }
}