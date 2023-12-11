using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private VRControllerButtons button;
        [SerializeField] private bool both;
        [SerializeField] private bool left;
        [SerializeField] private bool right;
        [SerializeField] private TutorialData tutorialData;
        [SerializeField] private UnityEvent tutorialSetUp;

        public void SetUpHighlightAndAnimation()
        {
            tutorialData.button = button;
            tutorialData.both = both;
            tutorialData.left = left;
            tutorialData.right = right;
            tutorialSetUp.Invoke();
        }
    }
}