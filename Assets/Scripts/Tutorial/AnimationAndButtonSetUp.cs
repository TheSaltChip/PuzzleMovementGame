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
        ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
    }

    public void End()
    {
        ControllerL.SetActive(false);
        ControllerR.SetActive(false);
    }
}
