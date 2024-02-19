using System;
using UnityEngine;
using Variables;

namespace Tutorial
{
    public class TutorialList : MonoBehaviour
    {
        [SerializeField] private TutorialStep[] tutorialSteps;
        [SerializeField] private IntVariable length;
        private int _current;

        private void Start()
        {
            length.value = tutorialSteps.Length;
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
