using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private VRControllerButtons button;
        [SerializeField] private SelectedHand selectedHand;
        [SerializeField] private TutorialData tutorialData;
        [SerializeField] private UnityEvent tutorialSetUp;

        public void SetUpHighlightAndAnimation()
        {
            tutorialData.button = button;
            tutorialData.selectedHand = selectedHand;
            tutorialSetUp.Invoke();
        }
    }
}