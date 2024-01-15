using Tutorial;
using UnityEngine;

public class AnimationAndButtonSetUp : MonoBehaviour
{
    [SerializeField] private GameObject ControllerL;
    [SerializeField] private GameObject ControllerR;
    [SerializeField] private TutorialData tutorialData;

    private void Start()
    {
        ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
    }

    public void Activate()
    {
        if (tutorialData.selectedHand == SelectedHand.Both)
        {
            ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }else if (tutorialData.selectedHand == SelectedHand.Right)
        {
            ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }
        else if (tutorialData.selectedHand == SelectedHand.Left)
        {
            ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }
    }
}
