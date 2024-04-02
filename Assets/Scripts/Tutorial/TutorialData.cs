using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "Tutorial/TutorialData", order = 0)]
    public class TutorialData : ScriptableObject
    {
        public VRControllerButtons button;
        public SelectedHand selectedHand;
        public bool skipAnimation;
    }
}