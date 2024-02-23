using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    [CreateAssetMenu(menuName = "Tutorial/TutorialStep", fileName = "TutorialStep")]
    public class TutorialStep : ScriptableObject
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