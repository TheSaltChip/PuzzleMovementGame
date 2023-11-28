using UnityEngine;
using UnityEngine.UI;

namespace UI.ThrowOnTarget
{
    [CreateAssetMenu(fileName = "SelectedLevelReference", menuName = "SelectedLevelReference", order = 0)]
    public class SelectedLevelReference : ScriptableObject
    {
        public ReferenceToLevelFile ReferenceToLevelFile;
    }
}