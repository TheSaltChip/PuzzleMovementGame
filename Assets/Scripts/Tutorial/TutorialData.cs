using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialData", menuName = "Tutorial/TutorialData", order = 0)]
    public class TutorialData : ScriptableObject
    {
        public VRControllerButtons button;
        public bool both;
        public bool left;
        public bool right;
    }
}