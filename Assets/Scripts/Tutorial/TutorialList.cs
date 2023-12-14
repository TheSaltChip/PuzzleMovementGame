using UnityEngine;

namespace Tutorial
{
    public class TutorialList : MonoBehaviour
    {
        [SerializeField] private TutorialStep[] tutorialSteps;
        private int _current;

        public void NextStep()
        {
            if (_current >= tutorialSteps.Length)
                return;
            _current++;
            tutorialSteps[_current].SetUpHighlightAndAnimation();
        }

        public void PreviousStep()
        {
            if (_current < 0)
                return;
            _current--;
            tutorialSteps[_current].SetUpHighlightAndAnimation();
        }
    
    }
}
