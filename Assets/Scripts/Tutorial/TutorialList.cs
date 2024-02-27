using UnityEngine;
using Variables;

namespace Tutorial
{
    public class TutorialList : MonoBehaviour
    {
        [SerializeField] private TutorialStep[] tutorialSteps;
        [SerializeField] private IntVariable length;
        [SerializeField] private TutorialData data;
        private int _current;

        private void Awake()
        {
            length.value = tutorialSteps.Length;
            data.selectedHand = SelectedHand.Both;
            data.button = VRControllerButtons.Trigger;
        }

        public void NextStep()
        {
            if (_current >= tutorialSteps.Length)
            {
                return;
            }
            _current++;
            tutorialSteps[_current].SetUpHighlightAndAnimation();
        }

        public void PreviousStep()
        {
            if (_current < 0)
            {
                return;
            }
            _current--;
            tutorialSteps[_current].SetUpHighlightAndAnimation();
        }
    
    }
}
